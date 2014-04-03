using Roslyn.Compilers.CSharp;
using Roslyn.Compilers.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RoslynTreeViewer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            //this.fileNameTB.Text = @"C:\SampleCode\TestClass.cs";
            //this.fileNameTB.Text = @"C:\SampleCode\TestClassWithProperties.cs";
            //this.fileNameTB.Text = @"C:\SampleCode\euler9.cs";
            //this.fileNameTB.Text = @"C:\SampleCode\Euler1Linq.cs";
            //this.fileNameTB.Text = @"..\..\MainWindow.xaml.cs";
            this.fileNameTB.Text = @"D:\Projects\CMCS E-Services\Trunk\App_Code\aes.vb";




        }

        private void AddNodeToTree(Roslyn.Compilers.CSharp.SyntaxNode codeNode, ItemsControl parent)
        {
            var newNode = new TreeViewItem();

            newNode.Header = codeNode.GetType().Name;
            newNode.ToolTip = codeNode.ToFullString();

            parent.Items.Add(newNode);

            foreach (var childCodeName in codeNode.ChildNodes())
            {
                AddNodeToTree(childCodeName, newNode);
            }
        }

        private void AddNodeToTree(Roslyn.Compilers.VisualBasic.SyntaxNode codeNode, ItemsControl parent)
        {
            var newNode = new TreeViewItem();

            newNode.Header = codeNode.GetType().Name;
            newNode.ToolTip = codeNode.ToFullString();

            parent.Items.Add(newNode);

            foreach (var childCodeName in codeNode.ChildNodes())
            {
                AddNodeToTree(childCodeName, newNode);
            }
        }

        private void treeControl_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            var selectedItem = e.NewValue as TreeViewItem;
            textControl.Text = selectedItem.ToolTip.ToString();
        }

        private void AnalyzeBtn_Click(object sender, RoutedEventArgs e)
        {
            switch (System.IO.Path.GetExtension(this.fileNameTB.Text.ToLower()))
            {
                case ".vb":
                    var vbTree = Roslyn.Compilers.VisualBasic.SyntaxTree.ParseFile(this.fileNameTB.Text);
                    var vbCompilationUnit = (Roslyn.Compilers.VisualBasic.CompilationUnitSyntax)vbTree.GetRoot();
                    AddNodeToTree(vbCompilationUnit, treeControl);
                    break;
                case ".cs":
                    var csTree = Roslyn.Compilers.CSharp.SyntaxTree.ParseFile(this.fileNameTB.Text);
                    var csCompilationUnit = (Roslyn.Compilers.CSharp.CompilationUnitSyntax)csTree.GetRoot();
                    AddNodeToTree(csCompilationUnit, treeControl);
                    break;
            }
        }
    }
}