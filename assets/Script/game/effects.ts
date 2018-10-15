const shake = (node: cc.Node, duration: number) => {
  node.runAction(
    cc.repeatForever(
      cc.sequence(
        cc.moveTo(0.02, cc.v2(5, 7)),
        cc.moveTo(0.02, cc.v2(-6, 7)),
        cc.moveTo(0.02, cc.v2(-13, 3)),
        cc.moveTo(0.02, cc.v2(3, -6)),
        cc.moveTo(0.02, cc.v2(-5, 5)),
        cc.moveTo(0.02, cc.v2(2, -8)),
        cc.moveTo(0.02, cc.v2(-8, -10)),
        cc.moveTo(0.02, cc.v2(3, 10)),
        cc.moveTo(0.02, cc.v2(0, 0))
      )
    )
  );

  setTimeout(() => {
    node.stopAllActions();
    node.setPosition(0, 0);
  }, duration * 1000);
}


export {
  shake
}