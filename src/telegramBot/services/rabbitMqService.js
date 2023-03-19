import rabbit from "amqplib"

const rabbitMqService = {
    initializeConsumer : async (eventEmitter) => {
        let connection = await rabbit.connect({ host: "localhost" });
        let channel = await connection.createChannel();

        channel.consume("success_payment_telegram", async (m) => {
            let decoder = new TextDecoder('utf-8');
            let decodedMessage = decoder.decode(m.content);
            let userData = JSON.parse(decodedMessage);
            channel.ack(m);
            eventEmitter.emit("success_payment_telegram", userData);
        });
    }
}

export default rabbitMqService;