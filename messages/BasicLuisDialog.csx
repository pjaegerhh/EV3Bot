using System;
using System.Threading.Tasks;

using Microsoft.Bot.Builder.Azure;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Luis;
using Microsoft.Bot.Builder.Luis.Models;

using System.Net;
using System.Text;


// For more information about this template visit http://aka.ms/azurebots-csharp-luis


[Serializable]
public class BasicLuisDialog : LuisDialog<object>
{
    public BasicLuisDialog() : base(new LuisService(new LuisModelAttribute(Utils.GetAppSetting("LuisAppId"), Utils.GetAppSetting("LuisAPIKey"))))
    {
    }

    [LuisIntent("None")]
    public async Task NoneIntent(IDialogContext context, LuisResult result)
    {
        await context.PostAsync($"Diesen Befehl verstehe ich nicht: {result.Query}"); 
        context.Wait(MessageReceived);
    }

    // Go to https://luis.ai and create a new intent, then train/publish your luis app.
    // Finally replace "MyIntent" with the name of your newly created intent in the following handler



    [LuisIntent("Start")]
    public async Task StartIntent(IDialogContext context, LuisResult result)
    {
        await context.PostAsync($"You have reached the Start intent. \n You said: {result.Query}"); 
//        await context.PostAsync("Hallo und herzlich willkommen beim EV3 Bot !"); 
//        await context.PostAsync("Wenn Du Hilfe brauchst sage "Hilfe" oder "Help"); 
        context.Wait(MessageReceived);
    }

    [LuisIntent("pjEV3_help")]
    public async Task HelpIntent(IDialogContext context, LuisResult result)
    {
        await context.PostAsync($"Hilfe !. You said: {result.Query}"); 
        context.Wait(MessageReceived);
    }


    [LuisIntent("Fahren")]
    public async Task FahrenIntent(IDialogContext context, LuisResult result)
    {
        await context.PostAsync($"You have reached the Fahren intent. You said: {result.Query}"); 
        context.Wait(MessageReceived);
        var request = (HttpWebRequest)WebRequest.Create("http://csev3lego.azurewebsites.net/move/forward");
        var response = (HttpWebResponse)request.GetResponse();
        var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
//        await context.PostAsync($"You have reached the Help intent. You said: {responseString}"); 
//        context.Wait(MessageReceived);

    }
}