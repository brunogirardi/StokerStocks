using System;
using System.Globalization;
using System.Windows.Data;

namespace StokerStocks
{ 
    public class OperaçãoConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch ((Operações)value)
            {
                case Operações.Compra:
                    return "Compra";
                case Operações.Venda:
                    return "Venda";
                case Operações.Provento:
                    return "Provento";
                case Operações.Agrupamento:
                    return "Agrupamento";
                default:
                    return "Indefinido";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch (value)
            {
                case "Compra":
                    return Operações.Compra;
                case "Venda":
                    return Operações.Venda;
                case "Provento":
                    return Operações.Provento;
                case "Agrupamento":
                    return Operações.Agrupamento;
                default:
                    return Operações.Compra;
            }
        }
    }   
}
