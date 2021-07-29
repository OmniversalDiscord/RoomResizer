using System;
using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.SlashCommands;
using Microsoft.Extensions.Logging;
using RoomResizer;

var discord = new DiscordClient(new DiscordConfiguration()
{
    Token = Environment.GetEnvironmentVariable("DISCORD_TOKEN"),
    TokenType = TokenType.Bot,
    Intents = DiscordIntents.Guilds | DiscordIntents.GuildVoiceStates
});

var slash = discord.UseSlashCommands();
slash.RegisterCommands<Commands>();

await discord.ConnectAsync();

discord.Ready += (sender, _) =>
{
    sender.Logger.LogInformation($"Connected to Discord as {discord.CurrentUser.Username}#{discord.CurrentUser.Discriminator}");
    return Task.CompletedTask;
};

await Task.Delay(-1);