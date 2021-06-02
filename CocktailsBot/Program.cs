using System;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Bot.Types;
using WebApplication1;
using CocktailsBot.Commands;
using System.Collections.Generic;

namespace CocktailsBot
{
    class Program
    {
        static TelegramBotClient Bot;
        public static List<Command> commands;
        static void Main(string[] args)
        {

            Bot = new TelegramBotClient(Constants.token);

            //var updates = await bot.GetUpdatesAsync();

            Bot.OnMessage += BotOnMessegeReceived;
            //Bot.OnCallbackQuery += BotOnCallbackQueryReceived;
            var me = Bot.GetMeAsync().Result;
            commands = new List<Command>();
            commands.Add(new Start());           
            commands.Add(new Discount());
            commands.Add(new GetListCategoryCoctails());
            commands.Add(new GetListCategoryDishes());
            commands.Add(new GetListCoctailsByCategory());
            commands.Add(new GetListDishesByCategory());
            commands.Add(new GetDishById());
            commands.Add(new GetListLastDishes());
            commands.Add(new GetListDishes());
            commands.Add(new GetListLastCoctails());
            commands.Add(new GetListCoctails());
            commands.Add(new GetListPopularCoctails());
            commands.Add(new AddDishOrCoctailForOrder());
            commands.Add(new GetOrder());
            commands.Add(new GetOrderStatus());
            commands.Add(new MakeOrder());






            Console.WriteLine(me.FirstName);

            Bot.StartReceiving();
            
            Console.ReadLine();
            Bot.StopReceiving();
            
        }


        public static  void BotOnMessegeReceived(object sender, MessageEventArgs e)
        {
            var message = e.Message;

            Console.WriteLine(5);
            
            if (message.Type != MessageType.Text)
                return;

            string name = $"{message.From.FirstName} {message.From.LastName}";
            
            Console.WriteLine($"{name} : {message.Text}");

            foreach (Command command in commands)
            {
                if (message.Text == command.Name)
                {
                    foreach (Command com in commands)
                    {
                        if (com.IsWorking == true)
                            com.EndComand();
                    }

                    command.IsWorking = true;
                     command.Execute(Bot, message, commands);
                    
                    break;
                }
            }
            //Bot.OnMessage += BotOnMessegeReceived;
        }
    }
}
