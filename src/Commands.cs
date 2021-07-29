using System;
using System.Linq;
using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;

namespace RoomResizer
{
    public class Commands : SlashCommandModule
    {
        [SlashCommand("loungesize", "Sets the user limit for lounge")]
        public async Task Resize(InteractionContext ctx, [Option("size", "The new lounge size")] long size)
        {
            var lounge = await ctx.Client.GetChannelAsync(ulong.Parse(Environment.GetEnvironmentVariable("LOUNGE_ID")));
            var member = ctx.Member;
            var isAdmin = member.Roles.Any(r => r.Name == "Coffee Crew");
            var isConnected = member.VoiceState != null && member.VoiceState.Channel.Id == lounge.Id;

            if (!isAdmin && !isConnected)
            {
                await ctx.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource,
                    new DiscordInteractionResponseBuilder()
                        .AsEphemeral(true)
                        .WithContent("You must be connected to lounge in order to change its size!"));
                return;
            }

            var connectedUsersCount = lounge.Users.Count();

            if (size < connectedUsersCount || size > 99)
            {
                await ctx.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource,
                    new DiscordInteractionResponseBuilder()
                        .AsEphemeral(true)
                        .WithContent($"Invalid size! Size must be between {connectedUsersCount} and 99."));
            }

            await lounge.ModifyAsync(model => model.Userlimit = (int) size);
            await ctx.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource,
                new DiscordInteractionResponseBuilder().WithContent($"Resized the lounge limit to {size} user{(size == 1 ? "" : "s")}!"));
        }
    }
}