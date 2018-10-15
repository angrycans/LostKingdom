import Async from 'async'
import { shake } from 'game/effects'

const { ccclass, property } = cc._decorator;
@ccclass
export default class Home extends cc.Component {

    @property(cc.Button)
    button: cc.Button = null

    @property(cc.Button)
    buttonOne: cc.Button = null

    @property(cc.Button)
    buttonTwo: cc.Button = null

    @property(cc.Node)
    rootNode: cc.Node = null;

    home_width: number;
    home_height: number;
    assets = { img: {} }

    config = {
        "background": [
            { name: "home/bg1", anchor: [0.5, 0], x: 0, y: 0, parallax: 0.4, scale: true },
            { name: "home/bg2", anchor: [0.5, 0.5], x: 0, y: 340, parallax: 0.8, scale: true },
            { name: "home/bg3", anchor: [0.5, 0.5], x: 0, y: 330, parallax: 1.2, scale: true },
            { name: "home/bg4", anchor: [0.5, 0.5], x: 0, y: 300, parallax: 1.6, scale: true },
            { name: "home/bg5", anchor: [0.5, 0], x: 0, y: 140, parallax: 2.0, scale: true },
            { name: "home/bg6", anchor: [0.5, 0], x: 0, y: 150, parallax: 3.0, scale: false },
            { name: "home/bg7", anchor: [0.5, 0], x: 0, y: 250, parallax: 3.0, scale: false },
            { name: "home/bg8", anchor: [1, 0], x: 900, y: 0, parallax: 3.2, scale: false },
            { name: "home/bg9", anchor: [0, 0], x: -900, y: -150, parallax: 3.2, scale: false },


        ]
    }


    async onLoad() {
        cc.log("home onload", cc.winSize)
        this.home_width = cc.winSize.width * 3;
        this.home_height = cc.winSize.height
        //cc.director.getScene().getChildByName('Canvas').alignWithScreen();

        this.rootNode.setAnchorPoint(0.5, 0)
        //this.rootNode.active = true;
        // this.rootNode.x = 0;
        // this.rootNode.y = 0;
        // this.rootNode.width = 200;
        // this.rootNode.height = 200;
        // this.rootNode.setAnchorPoint(0, 0)
        // this.rootNode.color = cc.Color.RED;

        // this.rootNode.addComponent(cc.Label);
        // this.rootNode.getComponent(cc.Label).string = "rootnode"

        // this.dragNode.on(cc.Node.EventType.TOUCH_START, function () {
        //     cc.log('Drag stated ...');

        // }, this.node);

        // this.dragNode.on(cc.Node.EventType.TOUCH_END, function () {
        //     cc.log('Drag end ...');
        // }, this.node);
        this.node.on(cc.Node.EventType.TOUCH_MOVE, (event: cc.Event.EventTouch) => {

            // cc.log(event)
            let delta = event.touch.getDelta();
            //this.rootNode.x += delta.x;
            //cc.log(delta.x)

            for (let i = 0; i < this.config.background.length; i++) {
                this.rootNode.children[i].x += delta.x + Math.sign(delta.x) * this.config.background[i].parallax;
            }

        }, this.node);


        this.button.node.on('click', this.onBtnClicked);
        this.buttonOne.node.on('click', () => { this.onBtnClicked(1) });

        this.buttonTwo.node.on('click', () => { this.onBtnClicked(2) });



    }

    async loadAssets(config: any): Promise<any> {
        let self = this;

        return new Promise<any>((resolve, reject) => {
            let ret = {};
            Async.each(config.background, function (item, callback) {
                cc.loader.loadRes(item.name, cc.SpriteFrame, (err, spriteFrame) => {
                    if (err) {
                        callback(err)
                    }
                    ret[item.name] = spriteFrame
                    //cc.log(spriteFrame.getRect())
                    callback();

                });

            }, function (err) {
                if (err) {
                    cc.log('loadAsset err', err);
                    reject(err)
                } else {
                    resolve(ret)
                    cc.log('loadAsset ok', ret);
                }
            });
        });


    }

    async start() {
        cc.log("home start")
        this.assets.img = await this.loadAssets(this.config);

        cc.log("assets", this.assets);

        this.config.background.forEach(item => {
            //cc.log(item);
            let node = new cc.Node(item.name);
            node.setAnchorPoint(item.anchor[0], item.anchor[1])
            let bg: cc.Sprite = node.addComponent(cc.Sprite);

            bg.spriteFrame = this.assets.img[item.name];
            if (item.scale) {
                node.setContentSize(this.home_width, bg.spriteFrame.getRect().height)
            }

            node.x = item.x
            node.y = item.y
            this.rootNode.addChild(node);
        });


    }


    onBtnClicked = (dot?: number) => {
        console.log('button clicked!', dot);

        if (dot == 1) {
            this.rootNode.runAction(
                cc.moveTo(0.5, cc.v2(0, 0))
            )
        } else if (dot == 2) {
            this.rootNode.runAction(
                cc.moveTo(0.5, cc.v2(640, 0))
            )
        }
        //  shake(this.rootNode, 3);
        //this.display.textKey = i18n.t("cases/02_ui/03_button/SimpleButton.js.1");
    }
    // update (dt) {}
}
