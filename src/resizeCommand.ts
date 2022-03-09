import { Client, CommandInteraction, VoiceChannel, GuildMember, Guild } from "discord.js";

export const resizeCommand = async (client: Client, interaction: CommandInteraction, channelId: string) => {
    const { value: size } = <{value: number}>interaction.options.get('size');
    const lounge = <VoiceChannel>await client.channels.fetch(channelId);
    const member = <GuildMember>interaction.member;
    const isAdmin = member.roles.cache.find(r => r.name === "Coffee Crew");

    if (!isAdmin && member.voice.channelId !== lounge.id) {
        await interaction.reply({ content: "You must be connected to lounge in order to change its size!", ephemeral: true });
        return;
    }

    const loungeCount = lounge.members.size;

    if (size < loungeCount || size > 99) {
        await interaction.reply({ content: `Invalid size! Size must be between ${loungeCount} and 99.`, ephemeral: true });
        return;
    }

    lounge.setUserLimit(size);
    await interaction.reply(`Resized the lounge limit to ${size} user${size === 1 ? "" : "s"}!`);
}
