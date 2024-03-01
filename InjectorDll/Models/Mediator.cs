public class Mediator
{
    public event EventHandler UpdateButtonClicked;

    public void RaiseUpdateButtonClicked()
    {
        UpdateButtonClicked?.Invoke(this, EventArgs.Empty);
    }
}
