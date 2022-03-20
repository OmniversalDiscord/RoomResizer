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
        private async Task Resize(InteractionContext ctx, long size, ulong channelId)
        {
            var lounge = await ctx.Client.GetChannelAsync(channelId);
            var member = ctx.Member;
            var isAdmin = member.Roles.Any(r => r.Name == "Coffee Crew");
            var isConnected = member.VoiceState != null && member.VoiceState.Channel.Id == lounge.Id;

            if (!isAdmin && !isConnected)
            {
                await ctx.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource,
                    new DiscordInteractionResponseBuilder()
                        .AsEphemeral(true)
                        .WithContent("You must be connected the specified vc in order to change its size!"));
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

        [SlashCommand("loungesize", "Sets the user limit for lounge")]
        public async Task ResizeLounge(InteractionContext ctx, [Option("size", "The new lounge size")] long size)
        {
            await Resize(ctx, size, ulong.Parse(Environment.GetEnvironmentVariable("LOUNGE_ID")));
        }

        [SlashCommand("gamingsize", "Sets the user limit for gaming")]
        public async Task ResizeGaming(InteractionContext ctx, [Option("size", "The new gaming size")] long size)
        {
            await Resize(ctx, size, ulong.Parse(Environment.GetEnvironmentVariable("GAMING_ID")));
        }

        [SlashCommand("bingsize", "Sets the user limit for lounge")]
        public async Task ResizeBing(InteractionContext ctx, [Option("size", "The new Bing Chilling size")] long size)
        {
            await Resize(ctx, size, ulong.Parse(Environment.GetEnvironmentVariable("BING_CHILLING")));
        }
    }
}
