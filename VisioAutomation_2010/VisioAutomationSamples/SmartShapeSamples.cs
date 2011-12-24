﻿using VA = VisioAutomation;
using IVisio = Microsoft.Office.Interop.Visio;
using VisioAutomation.Extensions;
using System.Linq;
using System.Collections.Generic;

namespace VisioAutomationSamples
{
    public static class SmartShapeSamples
    {
        public static void ProgressBar()
        {
            var page_a = SampleEnvironment.Application.ActiveDocument.Pages.Add();


            // Draw some shapes
            var background = page_a.DrawRectangle(0, 0, 5, 1);
            var progress = page_a.DrawRectangle(0, 0, 1, 1);

            var background_fmt = new VA.Format.ShapeFormatCells();
            background_fmt.FillForegnd= "rgb(240,240,240)";
            background_fmt.LineColor = "rgb(100,100,100)";


            var progress_fmt = new VA.Format.ShapeFormatCells();
            progress_fmt.FillForegnd = "rgb(100,150,240)";
            progress_fmt.LineColor = "rgb(100,100,100)";

            // group the two shapes together
            page_a.Application.ActiveWindow.SelectAll();
            var group = page_a.Application.ActiveWindow.Selection.Group();

            // Set the progress shape update itself based on its position
            string bkname = background.NameID;
            var xform = new VA.Layout.XFormCells();
            xform.PinX = string.Format("GUARD({0}!PinX-{0}!LocPinX+LocPinX)", bkname);
            xform.PinY = string.Format("GUARD({0}!PinY)", bkname);
            xform.Width = string.Format("GUARD({0}!Width*(PAGENUMBER()/PAGECOUNT()))", bkname);
            xform.Height = string.Format("GUARD({0}!Height)", bkname); 

            var update = new VA.ShapeSheet.Update.SIDSRCUpdate();
            xform.Apply(update,progress.ID16);
            background_fmt.Apply(update,background.ID16);
            progress_fmt.Apply(update, progress.ID16);
            update.Execute(page_a);

            var markup1 = new VA.Text.Markup.TextElement();
            markup1.AppendField(VA.Text.Markup.Fields.PageName);
            markup1.AppendText(" (");
            markup1.AppendField(VA.Text.Markup.Fields.PageNumber);
            markup1.AppendText(" of ");
            markup1.AppendField(VA.Text.Markup.Fields.NumberOfPages);
            markup1.AppendText(") ");
            markup1.SetText(group);
        }
    }
}