const AV = require("leanengine");
const fs = require("fs");
const path = require("path");

/**
 * 加载 functions 目录下所有的云函数
 */
fs.readdirSync(path.join(__dirname, "functions")).forEach((file) => {
  require(path.join(__dirname, "functions", file));
});

/**
 * 一个简单的云代码方法
 */
AV.Cloud.define("hello", function (request) {
  return "Hello world!";
});

/**
 * 模拟抽卡请求
 */
AV.Cloud.define("draw", function (request) {
  const heros = [
    "刘备",
    "关羽",
    "张飞",
    "赵云",
    "黄忠",
    "曹操",
    "孙权",
    "夏侯惇",
    "周瑜",
    "董卓",
  ];
  const result = [];
  while (result.length < 5) {
    const random = Math.floor(Math.random() * heros.length);
    heros.slice(random, 1);
    result.push(heros[random]);
  }
  return result;
});
