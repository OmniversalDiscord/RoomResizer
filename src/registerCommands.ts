import { ApplicationCommandData, Client } from "discord.js";

const commandData: ApplicationCommandData[] = [
    {
        name: 'loungesize',
        description: 'Sets the user limit for lounge',
        options: [{
            name: 'size',
            type: 'INTEGER',
            description: 'The new lounge size',
            required: true
        }]
    },
    {
        name: 'gamingsize',
        description: 'Sets the user limit for gaming',
        options: [{
            name: 'size',
            type: 'INTEGER',
            description: 'The new gaming size',
            required: true
        }]
    }
]

export const registerGuild = async (client: Client, guildId: `${bigint}`) => {
    const commands = await client.guilds.cache.get(guildId)?.commands.set(commandData);
    console.log(`Registered ${commandData.length} guild command(s):`);
    console.log(commands);
}

export const registerGlobal = async (client: Client) => {
    const commands = await client.application?.commands.set(commandData);
    console.log(`Registered ${commandData.length} command(s):`);
    console.log(commands);
}
