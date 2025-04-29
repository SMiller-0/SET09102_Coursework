using SET09102_Coursework.ViewModels;

namespace SET09102_Coursework.Views;
    
public partial class NotePage : ContentPage
{
    public NotePage()
    {
        this.BindingContext = new NoteViewModel();
        InitializeComponent();
    }
}