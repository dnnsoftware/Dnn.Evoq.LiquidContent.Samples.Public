using System;
using System.Configuration;
using System.Windows.Forms;
using LiquidContentExplorer.Common;
using LiquidContentExplorer.DTO;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using StructuredContent.Models;

namespace LiquidContentExplorer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            JsonConvert.DefaultSettings =
                () =>
                {
                    var settings = new JsonSerializerSettings();
                    settings.Converters.Add(new StringEnumConverter { CamelCaseText = true });
                    return settings;
                };
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            treeViewLC.Dock = DockStyle.Fill;
            richTextNotes.Dock = DockStyle.Fill;

            textApiEndPoint.Text = ConfigurationManager.AppSettings["StructuredContent.Api"];
            textApiToken.Text = ConfigurationManager.AppSettings["StructuredContent.Token"];
        }

        private void btnClearContent_Click(object sender, EventArgs e)
        {
            textpiVersion.Clear();
            treeViewLC.Nodes.Clear();
            richTextNotes.Clear();
        }

        private void btnLoadContent_Click(object sender, EventArgs e)
        {
            using (var client = Utils.GetLiquidContentClient(textApiToken.Text))
            {
                var url = textApiEndPoint.Text;
                if (url.EndsWith("/")) url = url.Substring(0, url.Length - 1);

                try
                {
                    var version = ApiCalls.GetSetviceVersion(client, url);
                    textpiVersion.Text = version.Version;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                treeViewLC.Nodes.Clear();
                richTextNotes.Clear();

                try
                {
                    var contentTypes = ApiCalls.GetAllContentTypes(client, url);
                    PopulateTreeWithTypes(contentTypes);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                try
                {
                    var contentItems = ApiCalls.GetAllContentItems(client, url);
                    PopulateTreeWithItems(contentItems);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void PopulateTreeWithTypes(SearchResultDto<ContentType> contentTypes)
        {
            if (contentTypes != null)
            {
                treeViewLC.BeginUpdate();
                var rootNode = new TreeNode("Content Types");
                treeViewLC.Nodes.Add(rootNode);
                foreach (var doc in contentTypes.Documents)
                {
                    var node = new TreeNode(doc.Name);
                    rootNode.Nodes.Add(node);
                    var fieldsNode = new TreeNode("Fields");
                    foreach (var field in doc.Fields)
                    {
                        fieldsNode.Nodes.Add(JsonConvert.SerializeObject(field, Formatting.Indented));
                    }
                    doc.Fields = null;
                    doc.Properties = null;
                    node.Nodes.Add(JsonConvert.SerializeObject(doc, Formatting.Indented));
                    node.Nodes.Add(fieldsNode);
                    //UNDONE: add content type properties to each new node
                }
                treeViewLC.EndUpdate();
            }
        }

        private void PopulateTreeWithItems(SearchResultDto<ContentItem> contentItems)
        {
            if (contentItems != null)
            {
                treeViewLC.BeginUpdate();
                var rootNode = new TreeNode("Content Items");
                treeViewLC.Nodes.Add(rootNode);
                foreach (var doc in contentItems.Documents)
                {
                    var node = new TreeNode(doc.ContentTypeName + ": " + doc.Name);
                    rootNode.Nodes.Add(node);
                    node.Nodes.Add(JsonConvert.SerializeObject(doc, Formatting.Indented));
                    //UNDONE: add content item properties to each new node
                }
                treeViewLC.EndUpdate();
            }
        }

        private void treeViewLC_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            var node = e.Node;
            richTextNotes.Text = node.Text;
        }
    }
}
