using System;
using System.Text;

namespace TOCalculator
{
    public class Divider : CalculatorOperation
    {
        public override string Calculate()
        {
            StringBuilder sb = new StringBuilder();
            try
            {
                int total = Operands[0];
                sb.Append(Operands[0].ToString() + "/");
                for (int i = 1; i < Operands.Length; i++)
                {
                    total /= Operands[i];
                    sb.Append(Operands[i].ToString() + "/");
                }
                sb.Remove(sb.Length - 1, 1) //remove last '/'
                    .Append(" = " + total.ToString());
            }
            catch (DivideByZeroException ex)
            {
                sb.Clear().Append(ex.Message + " Try Again!");
            }
            return sb.ToString();
        }
    }
}
