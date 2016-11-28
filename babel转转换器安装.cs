1.新建一个工作目录，然后创建一个package.json文件，内容为：
	{
		"name": "my-project",
		"version": "1.0.0",
		"devDependencies": {
		}
	}
2.在git bash(或cmd 命令行)，在工作目录下执行命令安装babel-cli：
	$ npm  --save-dev install babel-cli 
	再安装一个全局的babel-cli：
	$ npm -g  install babel-cli
3.在工作目录中创建一个名字为.babelrc的文件(window系统中创建这种文件系统会提示：必须输入文件名，在git bash 中使用：$ touch .babelrc // 创建文件)，文件中内容为：
	{
		"presets": [
		"es2015"
		],
		"plugins": []
	}
4.安装babel-preset-es2015:
	$ npm install --save-dev babel-preset-es2015
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	