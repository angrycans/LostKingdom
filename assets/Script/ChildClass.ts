
import { acans } from "./kits"
import SuperClass from "./SuperClass";

import TMX from './games/tmx/index'
const { ccclass } = cc._decorator;


@ccclass
export default class ChildClass extends SuperClass {
    protected async testAsync(): Promise<string> {
        return new Promise<string>((resolve, reject) => {
            setTimeout(() => {
                let a = new acans.a();
                console.log("tmx 3=>", TMX);

                let tmx = new TMX();
                tmx.test()

                fetch('http://www.baidu.com', { mode: 'no-cors' })
                    .then(function (response) {
                        console.log(response);
                        return response.text()
                    }).then(function (body) {
                        document.body.innerHTML = body
                    })
                resolve("Hello2, World!555 From ChildClass!");
            }, 1000);
        });

    }
}