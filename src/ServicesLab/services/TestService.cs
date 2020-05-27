using System.Threading.Tasks;

namespace FundamentalsLab
{
    public class TestService
    {
        public Task<string> GetNameAsync(){
            return Task.FromResult("Mayko Estevez");
        }
    }
}