import { acans } from "../../kits"
const { ccclass } = cc._decorator;

//@ccclass
export default class TMX {

  init() {
    console.log("init");
  }

  public async test(): Promise<string> {
    return new Promise<string>((resolve, reject) => {
      //console.log("raw 2", cc.url.raw('test2.json'));
      cc.loader.loadRes('test2.json', function (err, res) {
        if (err) {
          cc.error(err.message || err);
          return;
        }
        cc.log('Result should be a prefab: ', res);

        resolve('ok');
      });

    });

  }
}
