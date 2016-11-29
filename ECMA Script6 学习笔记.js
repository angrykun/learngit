		ECMAScript 6入门
1.ECMAScript6 简介
	ECMAScript 6.0(以下简称ES6)是Javascript语言的下一代标准，已经在2015年6月正式发布了。它的目标是使得Javascript语言可以用来编写复杂的大型应用程序，成为企业级开发语言。

2.Babel转码器
	Babel是一个广泛使用的ES6转码器，可以将ES6代码转为ES5代码，从而在现有环境执行，这意味着，你可以使用ES6的方式编写程序，又不用担心现有环境是否支持。
	2.1命令行转码 babel-cli
		Babel提供babel-cli工具；
		安装转码工具：$ npm install --global babel-cli
		# 转码结果输出到标准输出
		$ babel example.js
		
		# 转码结果写入一个文件
		# --out-file 或 -o 参数指定输出文件
		$ babel example.js --out-file compiled.js
		# 或者
		$ babel example.js -o compiled.js
		
		# 整个目录转码
		# --out-dir 或 -d 参数指定输出目录
		$ babel src --out-dir lib
		# 或者
		$ babel src -d lib
		
		# -s 参数生成source map文件
		$ babel src -d lib -s
		说明：上面代码是在全局环境下，进行Babel转码。这意味着，如果项目要运行，全局环境必须有Babel，也就是说项目产生了对环境的依赖。另一方面，这样做也无法支持不同项目使用不同版本的Babel。
	2.2 babel-node
		babel-node 提供一个支持ES6的REPL环境。它支持Node的REPL环境的所有功能，而且可以直接运行ES6代码。
		$ babel-node //进入node环境
		$ babel-node es6.js  //运行es6脚本文件
	2.3 babel-register 
		babel-register 模块改写require命令，为它加上一个钩子。此后，每当使用require加载.js、.jsx、.es、。es6后缀名的文件，就会先用babel进行转码。
		$ npm install --save-dev babel-register 
		使用时，必须首先加载babel-register。
		require("babel-register");
		require("./index.js");
		然后，就不需要手动对index.js转码了。		
		需要注意的是，babel-register只会对require命令加载的文件转码，而不会对当前文件转码。另外，由于它是实时转码，所以只适合在开发环境使用。
	2.4 babel-core 
		如果某些代码需要调用Babel API进行转码，就需要使用babel-core模块。
		$ npm install babel-core --save //安装命令
	2.5 babel-polyfill 
		Babel默认只转换新的JavaScript句法（syntax），而不转换新的API，比如Iterator、Generator、Set、Maps、Proxy、Reflect、Symbol、Promise等全局对象，以及一些定义在全局对象上的方法（比如Object.assign）都不会转码。
		举例来说，ES6在Array对象上新增了Array.from方法。Babel就不会转码这个方法。如果想让这个方法运行，必须使用babel-polyfill，为当前环境提供一个垫片。
		$ npm install --save babel-polyfill  //为当前的环境提供一个垫片
		然后，在脚本头部，加入如下一行代码。
		import 'babel-polyfill';
		// 或者
		require('babel-polyfill');
3.浏览器环境
	Babel也可以用于浏览器环境，但是从Babel 6.0 开始，不在直接提供浏览器版本，而要用构建工具构建出来，如果你没有或不想使用构建工具，可以通过安装5.X版本的babel-core模块获取。
	运行上面的命令以后，就可以在当前目录的node_modules/babel-core/子目录里面，找到babel的浏览器版本browser.js（未精简）和browser.min.js（已精简）。然后，将下面的代码插入网页。
	<script src="node_modules/babel-core/browser.js"></script>
	<script type="text/babel">
	// Your ES6 code
	</script>
	上面代码中，browser.js是Babel提供的转换器脚本，可以在浏览器运行。用户的ES6脚本放在script标签之中，但是要注明type="text/babel"。
	
	另一种方法是使用babel-standalone模块提供的浏览器版本，将其插入网页。
	
	<script src="https://cdnjs.cloudflare.com/ajax/libs/babel-standalone/6.4.4/babel.min.js"></script>
	<script type="text/babel">
	// Your ES6 code
	</script>
	注意，网页中实时将ES6代码转为ES5，对性能会有影响。生产环境需要加载已经转码完成的脚本。
	
	$ npm install -g browserify //安装browserify 模块。
	$  browserify script.js -o bundle.js // 把ES6脚本script.js 转换成bundle.js 浏览器直接加载后者就可以了 
	在package.json设置下面的代码，就不用每次命令行都输入参数了。
	{
		"browserify": {
		"transform": [["babelify", { "presets": ["es2015"] }]]
		}
	}


4.let和const命令
	1.let命令
		1.1基本用法
			ES6新增 let 命令，用来声明变量。它的用法类似于 var,但是所有的变量，只在let命令所在的代码块内有效。
			①在bash中，使用'\'进行续行。
			②在bash中，清屏方法：
				方法一： console.log("\033[2J");
				方法二： window：ctrl+l ;mac:command+l;
		1.2不存在变量提升
			let 不像 var 那样会发生"变量提升"现象。所以变量一定要在声明之后使用，否则报错。
		1.3暂时性死区
			只要块级作用域内存在 let 命令，它所声明的变量就绑定(binding)这个区域，不再受外部的影响。
			ES6中明确规定，如果区块中存在 let 和 const 命令，这个区块对这些命令声明的变量，从一开始就形成了封闭作用域。凡是在声明之前就使用这些变量，就会报错。
			总之，在代码内，使用 let 命令声明变量之前，该变量都是不可用的。这在语法上，称为"暂时性死区"(temporal dead zone 简称TDZ)。
			"暂时性死区"也意味着 typeof 不再是一个百分之百安全的操作。在没有 let 之前， typeof 运算符是百分百安全的，用于不会报错，现在这一点不成立了。这样设计的目的是为了让大家养成良好的变成习惯，变量一定要在声明之后使用，否则报错。
			ES6规定暂时性死区和 let 、 const 语句不出现变量提升，主要是为了减少运行时错误，防止在变量声明前就使用这个变量，从而导致意料之外的行为。
			总之，暂时性死区的本质就是，只要一进入当前作用域，所有使用的变量就已经存在了，但是不可获取，只有等到声明变量的那一行代码出现，才可以获取和使用变量。
		1.4不允许重复声明
			let 不允许在相同的作用域内，重复声明同一个变量。
	2.块级作用域
		2.1为什么需要块级作用域
			2.1.1 ES5中只有全局作用域和函数作用域，没有块级作用域，这带来很多不合理的场景。
				第一种场景：内层变量可能会覆盖外层变量。
				第二种场景：用来计数的循环变量泄漏成为全局变量。
		2.2ES6中块级作用域
			let 为Javascript新增了块级作用域。
			ES6允许块级作用域的任意嵌套，{{{{{let insane = 'Hello World'}}}}};
			外层作用域无法读取内层作用域的变量；内层作用域可以定义外层作用域的同名变量。
			块级作用域的出现，使得广泛应用的立即执行匿名函数(IIFE)不再必要了。
	3.块级作用域与函数声明
		ES5中规定，函数只能在顶层作用域和函数作用域中声明，不能在块级作用域中声明。但是，浏览器没有遵守这个规定，还是支持在块级作用域中声明函数。严格模式下，会报错。
		ES6中引入块级作用域，明确允许在块级作用域中声明函数。并且，块级作用域之中，函数声明语句的行为类似与 let ，块级作用域之外不可引用。
		考虑到环境导致的行为差异太大，应该避免在块级作用域中声明函数，如果确实需要，也应该写成函数表达式，而不是函数声明语句。
		ES6的块级作用域允许声明函数的规则，只在使用大括号的情况下成立，如果没有使用大括号，就会报错。
		// 不报错
		'use strict';
		if (true) {
		  function f() {}
		} 
		// 报错
		'use strict';
		if (true)
		  function f() {}

	4.const命令
		const 声明一个只读常量。一旦声明，常量的值就不能改变。const声明的变量不得改变值，这意味着，const一旦声明变量，就必须立即初始化，不能留到以后赋值。
		const 的作用域与let命令相同：只在声明所在的块级作用域内有效。
		const 命令声明的常量也不提升，同样存在暂时性死区，只能在声明的位置后面使用。
		const 命令声明的常量，也与 let 一样不可以重复声明。
		对于符合型的变量，变量名不指向数据，而是指向数据所在的地址， const 命令指示保证变量名指向的地址不变，并不保证该地址的数据不变，所以讲一个对象声明为常量必须非常小心。
		如果真的想将对象冻结，应该使用 Object.freeze()方法。
	5.顶层对象的属性
		顶层对象，在浏览器环境中指的是 window 对象，在Node指的是 global 对象。ES5中，顶层对象的属性与全局变量等价。
		window.a=1;
		a;//1
		顶层对象的属性与全局变量挂钩的问题：
			1.首先是没法在编译时就报出未声明的错误，只有运行时才知道。
			2.程序员不知不觉中就创建了全局变量。
			3.顶层对象的属性是到处可以读写的，这非常不利于模块化编程。
		window对象有实体含义，指的是浏览器的窗口对象，顶层对象是有一个有实体含义的对象，也是不合适的。
		ES6规定，为了保持兼容性， var 命令和 function 命令声明的全局变量，依旧是顶层对象的属性；另一方面规定， let 命令、 const 命令、 class 命令声明的全局变量，不属于顶层对象的属性。ES6开始，全局变量将逐步与顶层对象的属性挂钩。
		
5.变量的解构赋值
	5.1数组的解构赋值
		5.1.1基本用法
			ES6允许一定模式，从数组和对象中提取值，对变量进行赋值，这被称为解构(Destructuring);
			ES6允许写成下面这样：
				var [a,b,c]=[1,2,3]; //可以从数组中提取值，按照对应位置，对变量赋值。
				本质上这种写法属于"模式匹配"，只要等号两边的模式相同，左边的变量就会被赋予对应的值。
				let [foo, [[bar], baz]] = [1, [[2], 3]];
				foo // 1
				bar // 2
				baz // 3

				let [ , , third] = ["foo", "bar", "baz"];  //在对应位留空跳过被解构数组中的某些元素
				third // "baz"

				let [x, , y] = [1, 2, 3];
				x // 1
				y // 3

				let [head, ...tail] = [1, 2, 3, 4]; //通过"不定参数"模式捕获数组中的所有尾随元素
				head // 1
				tail // [2, 3, 4]

				let [x, y, ...z] = ['a'];  //当访问空数组或越界访问数组时，对其解构与对其索引的行为一致，最终得到的结果都是：undefined
				x // "a"
				y // undefined
				z // []


















































































