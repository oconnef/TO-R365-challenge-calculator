using System.Text;

namespace TOCalculator
{
    public class Adder : CalculatorOperation
    {   
        public override string Calculate()
        {
            StringBuilder sb = new StringBuilder();
            int sum = 0;
            foreach (int i in Operands)
            {
                sum += i;
                sb.Append(i.ToString() + "+");
            }
            sb.Remove(sb.Length - 1, 1) //remove last '+'
                .Append(" = " + sum.ToString());

            return sb.ToString();
        }
    }
}
