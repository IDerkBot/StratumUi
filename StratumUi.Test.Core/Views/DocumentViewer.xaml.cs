using System.Windows;

namespace StratumUi.Test.Core.Views;

public partial class DocumentViewer
{
    public DocumentViewer() => InitializeComponent();

    private void DocumentViewer_OnLoaded(object sender, RoutedEventArgs e)
    {
        // string fileName = null;
        // string appPath = System.IO.Path.GetDirectoryName(Assembly.GetAssembly(typeof(App)).CodeBase);
        // fileName = appPath + @"\Files\Instruction.xps";
        // fileName = fileName.Remove(0, 6);
        // XpsDocument doc = new XpsDocument(fileName, FileAccess.Read);
        // DcViewer.Document = doc.GetFixedDocumentSequence();
    }
}