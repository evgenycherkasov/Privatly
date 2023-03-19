const texts = {
    mainMenu : {
        title : "Hi✌🏻 Its Privatly!\n\nWe will provide you with an excellent solution to your problems with access to blocked sites with best prices🖥\n\nLeeeeet's goooo🔥"
    },

    backButtons : {
        backButtonText : "🔙",
        mainMenuText : "Main menu🔙"
    },

    letsGoMenu : {
        buttonText : "Leeeeet's goooo🔥",
        title : "Our VPN are stable fast reliable and secure🧑‍💻⏫🏰🔐\n\nJust choose the most suitable option for you and start using!"
    },

    accountMenu : {
        buttonText : "My account",
        title : (userName, password, isSubscriptionActive, endDate) => `That's your account informationℹ️\n\nYour login: ${userName}\nYour password: ${password}\n\n${isSubscriptionActive ? `You have active subscription until ${endDate}👍` : 'You have not active subscription😔'}\n\nIf you lost some links or files you can press /download!\n\nIf you need help installing Privatly VPN you can use the /help command!`
    },

    paymentMenu : {
        buttonText : (price, plan) => `${plan} | ${price} rubles`,
        title : (price, plan, description) => `You сhoose ${plan} option!\n\n${description}\n\nPrice of this subscription is ${price} rubles`
    },

    downloadCommand : {
        text : (isSubscriptionActive) => `${isSubscriptionActive ? 'You can download application for your laptop or PC by link below🧑‍💻\n\n dropbox link to desktop app\n\nAlso, you can use .ovpn configuration to setup your OpenVPN Connect application via smartphone\n\nIf you need help installing Privatly VPN you can use the /help command!' : 'Sorry but you have not active subscription😔'}`
    },

    successPayment : {
        text : (login, password, endDate) => `***Congratulations!***\n\nYou have renewed your subscription until ${endDate}\n\nThat's your account informationℹ️\n\nYour login: ${login}\nYour password: ${password}\n\nYou can press /download to download your VPN application or .ovpn configuration!\n\nIf you need help installing Privatly VPN you can use the /help command!`
    },

    helpCommand : {
        text : (isSubscriptionActive) => `${isSubscriptionActive ? "To set up Privatly VPN you need to watch one of our two video tutorials\n\nFirst video instruction is for Privatly VPN for your laptop or PC\n\nSecond video instruction is for Privatly VPN for your smartphone\n\nIf you have any questions, you can contact our support @evgrcg" : 'Sorry but you have not active subscription😔'}` 
    }
}
export default texts;