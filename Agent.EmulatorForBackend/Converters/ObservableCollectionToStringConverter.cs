using Newtonsoft.Json.Linq;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows.Data;

namespace Agent.EmulatorForBackend.Converters;
public class ObservableCollectionToStringConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if(value is ObservableCollection<string> collection)
            return string.Join(";", collection);
        return string.Empty;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value == null) 
            return new ObservableCollection<string>();

        return new ObservableCollection<string>(
            value.ToString()
            .Split(";")
            .Select(x => x.Trim()));

    }
}
