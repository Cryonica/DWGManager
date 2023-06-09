using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Data;

namespace DWGManager
{
    public class TypeFilterConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is List<DWGFile> files && parameter is string selectedType)
            {
                if (selectedType == "Все")
                {
                    return files;
                }
                else
                {
                    return files.Where(file => !string.IsNullOrEmpty(file.FileType) && file.FileType == selectedType).ToList();
                }
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
