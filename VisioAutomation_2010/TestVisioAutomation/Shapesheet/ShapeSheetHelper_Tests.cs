using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VisioAutomation.Extensions;
using System.Linq;
using IVisio = Microsoft.Office.Interop.Visio;
using VA = VisioAutomation;
using System.Diagnostics;

namespace TestVisioAutomation
{
    [TestClass]
    public class ShapeSheetHelperTests_Query : VisioAutomationTest
    {
        private static VA.Metadata.MetadataDB mdx = VA.Metadata.MetadataDB.Load();

        [TestMethod]
        public void CellNameParsing()
        {
            var p1 = VA.ShapeSheet.ShapeSheetHelper.ParseCellName("EndArrow");
            Assert.AreEqual("EndArrow", p1.FullName);
            Assert.AreEqual("EndArrow", p1.FullNameWithoutIndex);
            Assert.AreEqual(false, p1.IsDotted);
            Assert.AreEqual(null,p1.NameLeftOfDot);
            Assert.AreEqual(null, p1.NameRightOfDot);
            Assert.AreEqual(false, p1.IsIndexed);
            Assert.AreEqual(null, p1.Index);
            
            var p2 = VA.ShapeSheet.ShapeSheetHelper.ParseCellName("Foo.Bar");
            Assert.AreEqual("Foo.Bar", p2.FullName);
            Assert.AreEqual("Foo.Bar", p2.FullNameWithoutIndex);
            Assert.AreEqual(true, p2.IsDotted);
            Assert.AreEqual("Foo", p2.NameLeftOfDot);
            //Assert.AreEqual("Bar", p2.NameRightOfDot);
            Assert.AreEqual(false, p2.IsIndexed);
            Assert.AreEqual(null, p2.Index);

            var p3 = VA.ShapeSheet.ShapeSheetHelper.ParseCellName("Foo[1]");
            Assert.AreEqual("Foo[1]", p3.FullName);
            Assert.AreEqual("Foo", p3.FullNameWithoutIndex);
            Assert.AreEqual(false, p3.IsDotted);
            Assert.AreEqual(null, p3.NameLeftOfDot);
            Assert.AreEqual(null, p3.NameRightOfDot);
            Assert.AreEqual(true, p3.IsIndexed);
            Assert.AreEqual("1", p3.Index);

            var p4 = VA.ShapeSheet.ShapeSheetHelper.ParseCellName("Foo.Bar[1]");
            Assert.AreEqual("Foo.Bar[1]", p4.FullName);
            Assert.AreEqual("Foo.Bar", p4.FullNameWithoutIndex);
            Assert.AreEqual(true, p4.IsDotted);
            Assert.AreEqual("Foo", p4.NameLeftOfDot);
            Assert.AreEqual("Bar", p4.NameRightOfDot);
            Assert.AreEqual(true, p4.IsIndexed);
            Assert.AreEqual("1", p4.Index);

            var p5 = VA.ShapeSheet.ShapeSheetHelper.ParseCellName("Foo[1.2]");
            Assert.AreEqual("Foo[1.2]", p5.FullName);
            Assert.AreEqual("Foo", p5.FullNameWithoutIndex);
            Assert.AreEqual(false, p5.IsDotted);
            Assert.AreEqual(null, p5.NameLeftOfDot);
            Assert.AreEqual(null, p5.NameRightOfDot);
            Assert.AreEqual(true, p5.IsIndexed);
            Assert.AreEqual("1.2", p5.Index);

            var p6 = VA.ShapeSheet.ShapeSheetHelper.ParseCellName("Foo.Bar[1.2]");
            Assert.AreEqual("Foo.Bar[1.2]", p6.FullName);
            Assert.AreEqual("Foo.Bar", p6.FullNameWithoutIndex);
            Assert.AreEqual(true, p6.IsDotted);
            Assert.AreEqual("Foo", p6.NameLeftOfDot);
            Assert.AreEqual("Bar", p6.NameRightOfDot);
            Assert.AreEqual(true, p6.IsIndexed);
            Assert.AreEqual("1.2", p6.Index);
        }

        [TestMethod]
        public void SpotCheckNameToSRCMapping()
        {
            var c1 = VA.ShapeSheet.ShapeSheetHelper.TryGetSRCFromName("EndArrow").Value;
            var c2 = VA.ShapeSheet.SRCConstants.EndArrow;
            Assert.AreEqual(c2, c1);
        }

        [TestMethod]
        public void CheckAutomationCellNamesAgainstVA()
        {
            // Validate that we are able to compute the correct name for a cell
            // given only, its SRC values
            // to do this: first build up an object with a lot of sections and then try finding 
            // each cell and checking against the name predicted by the code

            var app = GetVisioApplication();
            var documents = app.Documents;
            var doc1 = this.GetNewDoc();
            var page1 = doc1.Pages[1];

            // Create a shape manipulate it to have a lot of sections
            var shape1 = page1.DrawRectangle(0.3, 0, 2.5, 5);
            using (var s = app.CreateUndoScope())
            {
                // set basic formatting
                shape1.CellsU["FillForegnd"].FormulaU = "rgb(255,134,78)";
                shape1.CellsU["FillBkgnd"].FormulaU = "rgb(255,134,98)";

                // add custom properties
                VA.CustomProperties.CustomPropertyHelper.SetCustomProperty(shape1, "custprop2", "value1");

                // add user defined cells
                VA.UserDefinedCells.UserDefinedCellsHelper.SetUserDefinedCell(shape1, "UserDefinedCell1", "Value1", "Prompt1");
                var ctrl = new VA.Controls.ControlCells();
                ctrl.X = "Width*0.5";
                ctrl.Y = "Width*0.75";

                // add controls
                VA.Controls.ControlHelper.AddControl(shape1, ctrl);

                // add hyperlinks
                var h1 = shape1.Hyperlinks.Add();
                var h2 = shape1.Hyperlinks.Add();
                h1.Address = "http://microsoft/com";

                // add character and paragraph formatting
                var t0 = new VA.Text.Markup.TextElement();
                t0.CharacterFormat.FontSize = 36;
                var t01 = t0.AppendText("HELLO");
                var t1 = t0.AppendElement("W\nO\nR\nL\nD");
                t1.ParagraphFormat.Indent = 1.0;
                t1.CharacterFormat.FontSize = 15.0;
                t0.AppendText("FOOBR");
                t0.SetText(shape1);

                // add tab stops
                var stops = new[]
                                {
                                    new VA.Text.TabStop(0.1, VA.Text.TabStopAlignment.Left),
                                    new VA.Text.TabStop(0.2, VA.Text.TabStopAlignment.Right)
                                };

                VA.Text.TextFormat.SetTabStops(shape1, stops);

            }

            // Pause a moment
            System.Threading.Thread.Sleep(1000);

            var failures = new List<string>();
            var success = new List<string>();

            /*
            foreach (var md_sec in mdx.Sections)
            {
                short sec_index = (short) mdx.GetAutomationConstantByName(md_sec.Enum).GetValueAsInt();
                Debug.WriteLine(md_sec.DisplayName);
                var cells = mdx.Cells.Where(c => c.SectionIndex == md_sec.Enum).Where( c=>c.Object.Contains("shape")).ToList();
                foreach (var cellinfo in EnumCellsInSection(shape1, sec_index))
                {

                    if (cellinfo.NameVisioInterop != cellinfo.NameFromVisioAutomation)
                    {
                        string msg = string.Format(" {0}!={1}  {2}", cellinfo.NameVisioInterop,
                                                   cellinfo.NameFromVisioAutomation, cellinfo.SRC.ToString());
                        failures.Add(msg);
                       
                    }
                }
            }*/

            // these are the section indices to validate
            short[] section_indexes = new short[] { 
                (short)IVisio.VisSectionIndices.visSectionObject, 
                (short)IVisio.VisSectionIndices.visSectionCharacter 
            };

            foreach (var section_index in section_indexes)
            {
                
                var cellinfos = EnumCellsInSection(shape1, section_index).ToList();
                foreach (var cellinfo_src in cellinfos)
                {
                    // what does VA think the cell name is
                    var predicted_cell_name = VA.ShapeSheet.ShapeSheetHelper.TryGetNameFromSRC(cellinfo_src);

                    if (predicted_cell_name == "Char.Locale")
                    {
                        continue;
                    }

                    if (predicted_cell_name != null)
                    {
                        // based on the name VA determines retrieve the cell
                        var found_cell = TryGetCellsU(shape1, predicted_cell_name);

                        if (found_cell!=null)
                        {
                            // verify that the found cell matches the initial SRC
                            var found_cell_src = new VA.ShapeSheet.SRC(found_cell.Section, found_cell.Row, found_cell.Column);

                            if (!cellinfo_src.AreEqual(found_cell_src))
                            {
                                Assert.Fail("cells don't match");
                            }
                            else
                            {
                                success.Add(predicted_cell_name + " " + found_cell.Name);
                            }                        
                            
                        }
                        else
                        {
                            failures.Add(string.Format("Could not find cell with predicted name {0}",predicted_cell_name));
                        }
                    }
                    else
                    {
                        var unfound_cell_src = new VA.ShapeSheet.SRC(cellinfo_src.Section, cellinfo_src.Row, cellinfo_src.Cell);
                        var actual_cell =
                            shape1.CellsSRC[
                                (short) cellinfo_src.Section, (short) cellinfo_src.Row, (short) cellinfo_src.Cell];
                        failures.Add(string.Format("Missing predicted name for {0} - actual shapesheet name is {1} ", cellinfo_src.ToString(), actual_cell.Name));
                    }
                    
                }
            }

            Debug.WriteLine( string.Join("\r\n",success.ToArray()));
            if (failures.Count > 0)
            {
                string s = string.Join("\r\n", failures.ToArray());
                //Assert.Fail(s);
            }

            doc1.Close(true);
        }

        public IVisio.Cell TryGetCellsU(IVisio.Shape s, string n)
        {
            try
            {
                return s.CellsU[n];

            }
            catch (Exception)
            {
                return null;
            }
        }

        private IEnumerable<VA.ShapeSheet.SRC> EnumCellsInSection(IVisio.Shape shape, short section_index)
        {
            if (0 == shape.SectionExists[section_index, 1])
            {
                yield break;
            }
            var sec = shape.Section[section_index];


            foreach (short row_index in EnumRowIndicesForSection(shape,section_index))
            {
                var row = sec[row_index];
                int num_cells = shape.RowsCellCount[section_index,row_index];
                for (int c = 0; c < num_cells; c++)
                {
                    var cell = row[c];
                    var cell_name = cell.Name;
                    var cell_src = new VA.ShapeSheet.SRC(cell.Section, cell.Row, cell.Column);
                    yield return cell_src;
                }
            }
        }

        private short[] section_obj_rows = new short[]
                                               {
                                                    (short) IVisio.VisRowIndices.visRowFill,
                                                    (short) IVisio.VisRowIndices.visRowLine,
                                                    (short)IVisio.VisRowIndices.visRowXFormOut };

        private IEnumerable<short> EnumRowIndicesForSection(IVisio.Shape shape, short section_index)
        {
            if (section_index == (short)IVisio.VisSectionIndices.visSectionObject)
            {
                foreach (var row_index in section_obj_rows)
                {
                    if (shape.RowExists[(short)section_index, (short)row_index, (short)0] != 0)
                    {
                        yield return row_index;
                    }
                }
            }
            else
            {
                short num_rows = shape.RowCount[section_index];
                for (short row_index = 0; row_index < num_rows; row_index++)
                {
                    yield return row_index;
                }
            }
        }


    }
}