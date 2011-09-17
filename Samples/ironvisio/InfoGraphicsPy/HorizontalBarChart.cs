﻿using System;
using System.Collections;
using System.Collections.Generic;
using IVisio=Microsoft.Office.Interop.Visio;
using IG=InfoGraphicsPy;
using System.Linq;
using VA=VisioAutomation;
using VisioAutomation.Extensions;

namespace InfoGraphicsPy
{
    public class HorizontalBarChart : GridChart
    {
        public string[] CategoryLabels;
        public DataPoints DataPoints;
        new double CellHeight = 0.25;

        public HorizontalBarChart(DataPoints dps, string[] cats)
        {
            this.DataPoints = dps;
            this.CategoryLabels = cats;
            
        }

        public void Draw(Session session)
        {
            var cats = new[] { "A", "B", "C", "D", "E" };
            var datapoints = new DataPoints(new double[] { 1.0, 2.0, 3.0, 4.0, 5.0 });
            var normalized_values = datapoints.GetNormalizedValues();
            var heights = ConstructPositions(datapoints.Count(), CellHeight, this.VerticalSeparation);
            var widths = ConstructPositions(new[] { this.CategoryLabelHeight, this.CellWidth}, this.HorizontalSeparation);
            var grid = new GridLayout(widths, heights);

            int catcol = 0;
            int barcol = 2;

            var content_rects = this.SkipOdd(grid.GetRectsInCol(barcol)).ToList();

            var dom = new VA.DOM.Document();
            dom.ResolveVisioShapeObjects = true;

            var bar_rects = new List<VA.Drawing.Rectangle>(content_rects.Count);
            for (int i = 0; i < content_rects.Count; i++)
            {
                var r = content_rects[i];
                dom.DrawRectangle(r);
                var size = new VA.Drawing.Size(normalized_values[i] * r.Width, this.CellHeight);
                var bar_rect = new VA.Drawing.Rectangle(r.LowerLeft, size);
                bar_rects.Add(bar_rect);
            }

            var cat_rects = this.SkipOdd(grid.GetRectsInCol(catcol)).ToList();

            var bar_shapes = this.DrawRects(dom, bar_rects, session.MasterRectangle);
            var cat_shapes = this.DrawRects(dom, cat_rects, session.MasterRectangle);

            for (int i = 0; i < datapoints.Count; i++)
            {
                bar_shapes[i].Text = datapoints[i].Text.ToString();
                cat_shapes[i].Text = cats[i];
            }

            foreach (var shape in bar_shapes)
            {
                var cells = shape.ShapeCells;

                cells.FillForegnd = this.ValueFillColor;
                cells.LineColor = this.LineLightBorder;

            }

            foreach (var shape in cat_shapes)
            {
                var cells = shape.ShapeCells;

                cells.FillPattern = this.CategoryFillPattern;
                cells.LineWeight = this.CategoryLineWeight;
                cells.LinePattern = this.CategoryLinePattern;
            }
            dom.Render(session.Page);
        }
    }

}