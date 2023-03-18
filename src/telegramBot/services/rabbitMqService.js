import rabbit from "amqplib"

const rabbitMqService = {
    initializeConsumer : async (bot, apiService) => {
        let connection = await rabbit.connect({ host: "localhost" });
        let channel = await connection.createChannel();

        channel.consume("success_payment", async (m) => {
            let decoder = new TextDecoder('utf-8');
            console.log(m.content);
            let decodedMessage = decoder.decode(m.content);

            console.log(decodedMessage);
        });
    }
}

export default rabbitMqService;