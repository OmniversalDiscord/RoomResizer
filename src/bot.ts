import { Client, Interaction } from 'discord.js';
import { registerGlobal, registerGuild } from './registerCommands';
import { resizeCommand } from './resizeCommand';
require('dotenv').config();

const client = new Client({ intents: ['GUILDS', 'GUILD_VOICE_STATES'] });

client.once('ready', () => {
    console.log(`Logged in as ${client.user?.tag}`);
    
    // Command registration
    if (process.argv.length > 2) {
        switch(process.argv[2]) {
            case 'register':
                registerGlobal(client).then(() => { process.exit(0) });
            case 'register-guild':
                registerGuild(client, <`${bigint}`> process.env.TEST_GUILD).then(() => { process.exit(0) });
        }
    }
});

client.on('interactionCreate', async interaction => {
    if (!interaction.isCommand()) return;
    if (interaction.commandName === 'loungesize') await resizeCommand(client, interaction, '419355451277312000');
    else if (interaction.commandName === 'gamingsize') await resizeCommand(client, interaction, '951241315067240448');
})

if (process.env.DISCORD_TOKEN === undefined) {
    console.log("No discord token set!");
    process.exit(1);
}

client.login(process.env.DISCORD_TOKEN);
