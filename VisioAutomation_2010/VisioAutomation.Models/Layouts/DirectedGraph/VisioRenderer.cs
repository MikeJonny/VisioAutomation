using IVisio = Microsoft.Office.Interop.Visio;

namespace VisioAutomation.Models.Layouts.DirectedGraph
{
    class VisioRenderer
    {
        public VisioRenderer()
        {
        }

        public void Render(IVisio.Page page, DirectedGraphLayout directedGraphLayout, VisioLayoutOptions options)
        {
            // This is Visio-based render - it does NOT use MSAGL
            if (page == null)
            {
                throw new System.ArgumentNullException(nameof(page));
            }

            if (options== null)
            {
                throw new System.ArgumentNullException(nameof(options));
            }

            var page_node = new Dom.Page();
            double x = 0;
            double y = 1;
            foreach (var shape in directedGraphLayout.Shapes)
            {
                var shape_nodes = page_node.Shapes.Drop(shape.MasterName, shape.StencilName, x, y);
                shape.DOMNode = shape_nodes;
                shape.DOMNode.Text = new VisioAutomation.Models.Text.TextElement(shape.Label);
                x += 1.0;
            }

            foreach (var connector in directedGraphLayout.Connectors)
            {
                var connector_node = page_node.Shapes.Connect("Dynamic Connector", "connec_u.vss", connector.From.DOMNode, connector.To.DOMNode);
                connector.DOMNode = connector_node;
                connector.DOMNode.Text = new VisioAutomation.Models.Text.TextElement(connector.Label);
            }

            page_node.ResizeToFit = true;
            page_node.ResizeToFitMargin = new VisioAutomation.Drawing.Size(0.5, 0.5);
            if (options.Layout != null)
            {
                page_node.Layout = options.Layout;
            }
            page_node.Render(page);
        }
    }
}