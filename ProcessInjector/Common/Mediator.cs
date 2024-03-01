namespace ProcessInjector.Common
{
    public class Mediator
    {
        public EventHandler UpdateButtonClicked;
        public void RaiseButtonClicked()
        {
            UpdateButtonClicked?.Invoke(this, EventArgs.Empty);
        }
    }
}
