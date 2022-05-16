using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace WPF.ViewModel;

public class BindableBase
{
    public event PropertyChangedEventHandler? PropertyChanged;

    /// <summary>
    /// Trigger event <see cref="PropertyChanged"/> so the view will be updated
    /// </summary>
    /// <param name="propertyName"></param>
    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
    /// <summary>
    /// Change the storage value to the new value. This automatically fires <see cref="OnPropertyChanged"/>.
    /// Will return false if the value's are the same.
    /// </summary>
    /// <param name="storage"></param>
    /// <param name="value"></param>
    /// <param name="propertyName"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    protected virtual bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = "")
    {
        if (EqualityComparer<T>.Default.Equals(storage, value))
            return false;
        storage = value;
        this.OnPropertyChanged(propertyName);
        return true;
    }
}