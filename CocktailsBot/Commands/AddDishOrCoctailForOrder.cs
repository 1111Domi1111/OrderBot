using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Bot.Types.Enums;
using System.Collections.Generic;
using CocktailsBot.Clients;
using WebApplication1;

namespace CocktailsBot.Commands
{
    class AddDishOrCoctailForOrder : Command
    {
        public override bool IsWorking { get; set; } = false;
        public override TelegramBotClient Bot { get; set; }
        public override string Name { get; set; } = "/adddishorcoctailfororder";

        public static List<Command> commands { get; set; }

        public override async void Execute(TelegramBotClient _bot, Message e, List<Command> _commands)
        {
            commands = _commands;
            Bot = _bot;
            await Bot.SendTextMessageAsync(e.From.Id, "Введите 'dish' for dish or 'coctail' for coctail");
            Bot.OnMessage += GetString;
        }
        private async void GetString(object sender, MessageEventArgs e)
        {
            string Id = e.Message.Text;
            foreach (Command command in commands)
            {
                if (Id == command.Name)
                {
                    return;
                }
            }
            if(Id=="dish")
            {
                Bot.OnMessage += GetIdDish;
                await Bot.SendTextMessageAsync(e.Message.From.Id, "Enter the Id of the dish you want to order");
            }
           else if( Id=="coctail")
            {
                Bot.OnMessage += GetIdCoctail;
                await Bot.SendTextMessageAsync(e.Message.From.Id, "Enter the Id of the coctail you want to order");
            }




            EndComand();
        }
        private async void GetIdDish(object sender, MessageEventArgs e)
        {
            string Id = e.Message.Text;
            foreach (Command command in commands)
            {
                if (Id == command.Name)
                {
                    return;
                }
            }
            try
            {
                ApiClient api = new ApiClient();

              var  result = api.GetDish(Id);

                var result1 = api.AddDishOrCoctail($"{ e.Message.Chat.Id}", result.Result.Name, 1, true);
                await Bot.SendTextMessageAsync(e.Message.From.Id, $"dishAdd");
            }
         
             catch
            {
                await Bot.SendTextMessageAsync(e.Message.From.Id, "Incorrectly entered information");
            }
            


        }
        private async void GetIdCoctail(object sender, MessageEventArgs e)
        {
            string Id = e.Message.Text;
            foreach (Command command in commands)
            {
                if (Id == command.Name)
                {
                    return;
                }
            }
            try
            {
                ApiClient api = new ApiClient();
                var result = api.GetCoctail(Id);
                var result1 = api.AddDishOrCoctail($"{ e.Message.Chat.Id}", result.Result.StrDrink, 1, true);
                await Bot.SendTextMessageAsync(e.Message.From.Id, $"Order Add - ");
            }

            catch
            {
                await Bot.SendTextMessageAsync(e.Message.From.Id, "Incorrectly entered information");
            }



        }



        protected async void SendInf(Dishes results, Message message)
        {
            await Bot.SendTextMessageAsync(message.From.Id, $"{results.Name} - name dish, Id" +
                $" you want to order\n\n", parseMode: ParseMode.Html);
        }



        public override void EndComand()
        {
            IsWorking = false;
        }
    }
}