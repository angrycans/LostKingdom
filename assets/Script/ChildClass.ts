import SuperClass from "./SuperClass";
import {acans} from "./kits"
const {ccclass} = cc._decorator;

@ccclass
export default class ChildClass extends SuperClass {
    protected async testAsync(): Promise<string> {
        return new Promise<string>((resolve, reject) => {
            setTimeout(() => {
                let a =new acans.a();
                resolve("Hello2, World!555 From ChildClass!");
            }, 1000);
        });

    }
}
