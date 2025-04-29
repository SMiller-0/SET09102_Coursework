using SET09102_Coursework.ViewModels;

namespace SET09102_Coursework.Views;

public partial class AllNotesPage : ContentPage
{
    public AllNotesPage()
    {
        this.BindingContext = new NotesViewModel();
        InitializeComponent();
    }

    private void ContentPage_NavigatedTo(object sender, NavigatedToEventArgs e)
    {
        notesCollection.SelectedItem = null;
    }

}
