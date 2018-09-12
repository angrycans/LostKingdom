import Async from 'async'


const { ccclass, property } = cc._decorator;
@ccclass
export default class Home extends cc.Component {

    // LIFE-CYCLE CALLBACKS:
    @property(cc.Node)
    rootNode: cc.Node = null;
    assets = { img: {} }

    config = {
        "background": [
            { name: "home/bg1", anchor: [0.5, 0], x: 0, y: 0, parallax: 1 },
            { name: "home/bg2", anchor: [0.5, 0.5], x: 0, y: 340, parallax: 1 },
            { name: "home/bg3", anchor: [0.5, 0.5], x: 0, y: 330, parallax: 1 },
            { name: "home/bg4", anchor: [0.5, 0.5], x: 0, y: 300, parallax: 1 },
            { name: "home/bg5", anchor: [0.5, 0], x: 0, y: 140, parallax: 1 },
            { name: "home/bg6", anchor: [0.5, 0], x: 0, y: 150, parallax: 1 },
            { name: "home/bg7", anchor: [0.5, 0], x: 0, y: 250, parallax: 1 },
            { name: "home/bg8", anchor: [1, 0], x: 800, y: -50, parallax: 1 },
            { name: "home/bg9", anchor: [0, 0], x: -900, y: -150, parallax: 1 },


        ]
    }


    async onLoad() {
        cc.log("home onload", cc.winSize)
        //cc.director.getScene().getChildByName('Canvas').alignWithScreen();
        //canvas.alignWithScreen();
        //this.node.setAnchorPoint(1, 1)
        // this.rootNode = new cc.Node('rootNode');
        // this.node.setAnchorPoint(1, 1)
        this.rootNode.setAnchorPoint(0.5, 0)
        this.rootNode.active = true;
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

            cc.log(event)
            let delta = event.touch.getDelta();
            this.rootNode.x += delta.x;
            this.rootNode.y += delta.y;
            // this.rootNode.setPosition(cc.v2(this.node.x + delta.x, this.node.y + delta.y));

        }, this.node);



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
            cc.log(item);
            let node = new cc.Node('node_' + item.name);
            node.setAnchorPoint(item.anchor[0], item.anchor[1])
            let bg: cc.Sprite = node.addComponent(cc.Sprite);

            bg.spriteFrame = this.assets.img[item.name];
            node.x = item.x
            node.y = item.y
            this.rootNode.addChild(node);
        });


        // let node = new cc.Node('bg1node');
        // node.setAnchorPoint(0, 0)
        // node.color = new cc.Color(255, 0, 0)
        // this.rootNode.addChild(node);

        //let bg1: cc.Sprite = new cc.Sprite()
        //let bg2: cc.Sprite = this.addComponent(cc.Sprite);
        //self.node.setAnchorPoint(0.5, 0)
        //this.setPosition(cc.v2(500, 500))
        // cc.loader.loadRes("home/bg1", cc.SpriteFrame, (err, spriteFrame) => {

        //     cc.log(err, spriteFrame)
        //     if (err) {
        //         return
        //     }

        //     let node = new cc.Node('bg1node');
        //     node.setAnchorPoint(0, 0)
        //     let bg: cc.Sprite = node.addComponent(cc.Sprite);

        //     bg.spriteFrame = spriteFrame;

        //     // cc.log(node)
        //     this.rootNode.addChild(node);
        //     //cc.log(self.node)

        // });
        // cc.loader.loadRes("home/bg2", cc.SpriteFrame, (err, spriteFrame) => {

        //     cc.log(err, spriteFrame)
        //     if (err) {
        //         return
        //     }

        //     let node = new cc.Node('bg2node');
        //     node.setAnchorPoint(0, 0)
        //     let bg: cc.Sprite = node.addComponent(cc.Sprite);
        //     //  node.y = node.y - 23;

        //     bg.spriteFrame = spriteFrame;

        //     // cc.log(node)
        //     this.rootNode.addChild(node);
        //     //cc.log(self.node)

        // });
        // cc.loader.loadRes("home/bg20", cc.SpriteFrame, (err, spriteFrame) => {

        //     cc.log(err, spriteFrame)
        //     if (err) {
        //         return
        //     }

        //     let node = new cc.Node('bg20node');
        //     let bg: cc.Sprite = node.addComponent(cc.Sprite);

        //     bg.spriteFrame = spriteFrame;

        //     // cc.log(node)
        //     this.rootNode.addChild(node);
        //     //cc.log(self.node)

        // });
        // cc.loader.loadRes("home/bg21", cc.SpriteFrame, (err, spriteFrame) => {

        //     cc.log(err, spriteFrame)
        //     if (err) {
        //         return
        //     }

        //     let node = new cc.Node('bg21node');
        //     let bg: cc.Sprite = node.addComponent(cc.Sprite);

        //     bg.spriteFrame = spriteFrame;

        //     // cc.log(node)
        //     this.rootNode.addChild(node);
        //     //cc.log(self.node)

        // });

        // cc.loader.loadRes("home/bg3", cc.SpriteFrame, (err, spriteFrame) => {

        //     cc.log(err, spriteFrame)
        //     if (err) {
        //         return
        //     }

        //     let node = new cc.Node('bg3node');
        //     let bg: cc.Sprite = node.addComponent(cc.Sprite);

        //     bg.spriteFrame = spriteFrame;

        //     // cc.log(node)
        //     this.rootNode.addChild(node);
        //     //cc.log(self.node)

        // });
        // cc.loader.loadRes("home/bg4", cc.SpriteFrame, (err, spriteFrame) => {

        //     cc.log(err, spriteFrame)
        //     if (err) {
        //         return
        //     }

        //     let node = new cc.Node('bg4node');
        //     let bg: cc.Sprite = node.addComponent(cc.Sprite);

        //     bg.spriteFrame = spriteFrame;

        //     // cc.log(node)
        //     this.rootNode.addChild(node);
        //     //cc.log(self.node)

        // });
        // cc.loader.loadRes("home/bg5_1", cc.SpriteFrame, (err, spriteFrame) => {

        //     cc.log(err, spriteFrame)
        //     if (err) {
        //         return
        //     }

        //     let node = new cc.Node('bg5_1');
        //     let bg: cc.Sprite = node.addComponent(cc.Sprite);

        //     bg.spriteFrame = spriteFrame;

        //     // cc.log(node)
        //     this.rootNode.addChild(node);
        //     //cc.log(self.node)

        // });
        // cc.loader.loadRes("home/bg5_2", cc.SpriteFrame, (err, spriteFrame) => {

        //     cc.log(err, spriteFrame)
        //     if (err) {
        //         return
        //     }

        //     let node = new cc.Node('bg5_2');
        //     let bg: cc.Sprite = node.addComponent(cc.Sprite);

        //     bg.spriteFrame = spriteFrame;

        //     // cc.log(node)
        //     this.rootNode.addChild(node);
        //     //cc.log(self.node)

        // });
        // cc.loader.loadRes("home/bg2", cc.SpriteFrame, (err, spriteFrame) => {
        //     if (err) {
        //         return
        //     }
        //     bg2.spriteFrame = spriteFrame;

        // });

    }

    // update (dt) {}
}
