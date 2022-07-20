using Auth.Business.Service.Interfaces;
using Auth.Business.SmtpClientEmailSender.Interfaces;
using Auth.Business.ViewModels;
using System.Threading.Tasks;

namespace Auth.Business.Service
{
    public class CheckService : ICheckService
    {
        private readonly IEmailSender _emailSender;
        public CheckService(IEmailSender emailSender)
        {
            _emailSender = emailSender;
        }

        public async Task<bool> CheckSend(Check check)
        {
            var StringConfirm = "<html><head><style>p{color: black;}body{margin: auto;font-family: 'Nunito', sans-serif;font-family: 'Nunito Sans', sans-serif;}.content {height: 100%;width: 450px;font-size: 15px;margin: 0 auto; border: solid 2px; border-color: rgba(254, 161, 22);}.logo {background-color: rgba(254, 161, 22); color: #0F172B; font-weight: bold;font-size: 35px;}.box { width: 100%; display: flex; justify-content: space-between;} .heading { text-align: center;font-size: 18px;}</style></head><body> <div class=\"content\"><div class=\"logo\"><div style=\" margin - left: 20px;\"> AnySell</div> </div><div class=\"heading\">" + $"{check.StoreName}"+"</div><div class= \"heading\">"+$"{check.StoreAddress}"+"</div><h3 style = \"text-align: center;\"> Чек "+$"{check.OrderNumber}"+"</h3><div class= \"heading\" style = \"margin-bottom: 15px;\"> Кассир: "+$"{check.EmployeeFIO}"+ "</div><table style=\"width: 600px;\"><tbody>";
            var discount = 0.0;
            var totalSum = 0.0;
            foreach (var product in check.ReservationProducts)
            {
                var sum = (double)product.Price * product.Count;
                var discounProduct = (double)product.DiscountValue * product.Count;

                StringConfirm += $"<tr><td><h5>{product.Name}</h5><div class=\"box\"><div>{product.Price:##.00} X {product.Count}=</div><div style =\"margin-left:220px\">{sum:##.00} </div></div><div class=\"box\"><div>Скидка- </div><div style =\"margin-left:230px\">-{discounProduct:##.00}</div></div></td></tr>";
                discount += (double)product.DiscountValue * product.Count;
                totalSum += ((double)product.Price * product.Count) -((double)product.DiscountValue * product.Count);
            }
            StringConfirm += $"</tbody></table> <div class=\"box\"><div>Сумма</div><div style =\"margin-left:250px\">{totalSum:##.00}</div></div><div style=\"text-align: center;\">------Cлужебная информация------</div><hr><div class=\"box\"><div>Скидка</div><div style =\"margin-left:250px\">{discount:##.00}</div></div><hr><div>{check.OrderDate}</div></div></div></body></html>";

            return await _emailSender.SendEmailAsync(check.Email, "Check AnySell", StringConfirm);          
        }       
    }
}
