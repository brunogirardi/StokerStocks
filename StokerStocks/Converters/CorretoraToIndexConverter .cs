using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace StokerStocks
{
    class CorretoraToIndexConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {

            int index = 0;
            Corretoras corretora = (Corretoras)value;

            if (corretora == null) return -1;

            foreach (var item in MainRouter.Corretoras())
            {
                if (corretora.idCorretoras == item.idCorretoras) return index;
                index += 1;
            }

            return -1;

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return MainRouter.Corretoras()[(int)value];
        }
    }
}
