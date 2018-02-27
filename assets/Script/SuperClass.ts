
const { ccclass, property, executionOrder } = cc._decorator;

@ccclass
@executionOrder(1)
export default class SuperClass extends cc.Component {

    @property(cc.Label)
    label: cc.Label = null;

    @property
    text: string = 'hello';

    async onLoad() {
        // init logic
        console.log("SuperClass onLoad");
        this.label.string = await this.testAsync();
    }

    protected async testAsync(): Promise<string> {
        return new Promise<string>((resolve, reject) => {
            setTimeout(() => {
                console.log("SuperClass testAsync");
                resolve("Hello, World!");
            }, 1000)
        })
    }
}
