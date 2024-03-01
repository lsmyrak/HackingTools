using System.ComponentModel;
using System.Linq.Expressions;

namespace ProcessInjector.ViewModel;

public class ViewModelBase : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;
    protected virtual void OnPropertyChanged<T>(Expression<Func<T>> propertyExpression)
    {
        MemberExpression memberExpression = propertyExpression.Body as MemberExpression;
        if (memberExpression == null)
        {
            throw new ArgumentException("Wrong PropertyExpresion");
        }
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(memberExpression.Member.Name));
    }
}
