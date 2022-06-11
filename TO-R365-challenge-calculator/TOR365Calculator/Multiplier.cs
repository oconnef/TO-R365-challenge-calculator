using System.Text;

namespace TOCalculator
{
    public class Multiplier : CalculatorOperation
    {        
        public override string Calculate()
        {
            StringBuilder sb = new StringBuilder();
            int total = 1;
            for (int i = 0; i < Operands.Length; i++)
            {
                total *= Operands[i];
                sb.Append(Operands[i].ToString() + "*");
            }
            sb.Remove(sb.Length - 1, 1) //remove last '*'
                .Append(" = " + total.ToString());

            return sb.ToString();
        }
    }
}
