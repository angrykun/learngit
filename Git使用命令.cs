Git的使用命令
	1.安装好Git安装包之后，在开始菜单中找到 Git->Git Bash 弹出一个类似命令行窗口的东西，说明Git安装成功！
	 安装完成之后，还需要进行最后一步设置,在命令行输入
		$ git config --global user.name "Your Name"
		$ git config --global user.email "email@example.com"
	因为Git是分布式版本控制系统，所以每个机器必须自报家门：你的名字和Email地址。
	注意git config 命令的--global参数，用来这个参数之后，表示你这台机器上所有的Git仓库都会使用这个配置，当然也可以对某个仓库指定不同的用户名和Email地址。
	2.创建版本库(repository)
	 版本库又名仓库，可以简单理解成一个目录，这个目录里面的所有文件都可以被git管理起来，每个文件的修改，删除，git都能跟踪，以便任何时刻都可以追踪历史，或者在将来某个时刻可以"还原"；
	 2.1选择一个合适的地方创建一个目录：
		$ mkdir learngit
		$ cd learngit
		$ pwd	//显示当前的文件路径
		/User/michael/learngit
	 2.2通过git init 命令把这个目录变成git可以管理的仓库：
		$ git init //初始化Git仓库
		$ git add readme.txt //把文件添加到仓库
		$ git commit readme.txt -m "wrote a readme file"//把文件提交到仓库；-m 后面输入的是本次提交的说明，可以输入任意的内容，当然最好是有意义的，这样就可以从历史记录里方便的找到改动的记录。
		//commit 可以一次提交多个文件，所以你可以多次add 不同的文件
		$ git add file1.txt
		$ git add file2.txt file3.txt 
		$ git commit -m "add 3 files"
		$ git status //可以让我们掌握仓库当前的状态
		$ git diff	//查看文件不同的部分
		//提交更新的两个步骤，先add 然后commit提交更新。
		$ git add readme.txt
		$ git commit -m "add ...."
		
		$ git log //显示从最近到最远的提交日志
		$ git log --pretty=oneline //只显示Git commit版本号和Git提交的说明信息。
		$ git reset --hard HEAD^ //回退版本使用git reset 命令，HEAD 表示当前版本;HEAD^表示上一个版本，HEAD~100表示往上100个版本
								//回退之后，如果想回到最新的版本中，如果命令行窗口没有关掉的话，可以找到commit id，然后指定版本号进行回退。
		$ cat readme.txt //查看文件的内容 
		$ git reflog //记录着每一次的命令，确定回到未来的哪个版本。如果不知道回退的版本，此命令可以查看到操作的每一次命令。
	//工作区是可以看到的目录，比如learngit文件夹就是一个工作区
	//版本库里放了很多东西，其中最重要的就是成为stage(或者叫index)的暂存区，还有Git为我们自动创建的第一个分支master以及指向master的一个指针叫HEAD。
	//文件往Git版本库中添加的时候，是分两步执行的:
	//第一步就是用 git add 把文件添加到暂存区；
	//第二部就是用 git commit提交更改，实际上市把暂存区的所有内容提交到当前分支。
		$ git checkout --file //把文件在工作区的修改全部撤销，让文件回到最近一次git commit或git add时的状态。
		$ git reset HEAD file //可以把暂存区的修改撤销掉，重新放回工作区，HEAD表示最新的版本。
		//场景1：当你改乱了工作区某个文件的内容，想直接丢弃工作区的修改时，用命令git checkout -- file。
		//场景2：当你不但改乱了工作区某个文件的内容，还添加到了暂存区时，想丢弃修改，分两步，第一步用命令git reset HEAD file，就回到了场景1，第二步按场景1操作。
		//场景3：已经提交了不合适的修改到版本库时，想要撤销本次提交，参考版本回退一节，不过前提是没有推送到远程库。
		$ git rm fileName //用于删除一个文件。如果一个文件已经被提交到版本库，那么你永远不用担心误删，但是要小心，你只能恢复文件到最新版本，你会丢失最近一次提交后你修改的内容。
		$ git commit -m "remove fileName" //确定删除文件
	3.远程仓库
		3.1添加远程库
			首先登陆GitHub，然后在右上角找到"Create a new repository"按钮创建一个仓库：
			$ git remote add origin git@github.com:angrykun/testRepository.git //本地的仓库与远程库进行关联，远程库的名字就是origin，这是Git默认的叫法，也可以改成别的，但是origin这个名字一看就知道是远程库。
			$ git push -u origin master //把本地的所有内容推送到远程库上，实际上是把当前分支master推送到远程。
										//由于远程库是空的，我们第一次推送master分支时，加上了-u参数，Git不但会把本地的master分支内容推送的远程新的master分支，还会把本地的master分支和远程的master分支关联起来，在以后的推送或者拉取时就可以简化命令。
			$ git push origin master //只要本地做了提交，就可以通过此命令推送到远程库。
		3.2从远程库克隆
			$ git clone git@github.com:angrykun/learngit.git //克隆远程库，Git支持多种协议，包括https，但是通过ssh支持的原生git协议速度最快。
		3.3分支管理
			可以创建一个属于自己的分支，别人看不到，还继续在原来的分支上正常工作，而你在自己的分支上干活，想提交就提交，直到开发完毕后，在一次性合并到原来的分支上，这样既安全又不影响别人工作。
			HEAD严格来说并不是指向提交，而是指向master，master才指向提交的，所以HEAD指向的就是当前分支。
			mater分支是一条线，Git用master指向罪行的提交，再用HEAD指向master，就能确定当前分支，以及当前分支的提交点，每次提交，master分支都会向前移动异步，这样，随着你不断地提交，master分支的线，也越来越长。
			
			$ git checkout -b dev //创建一个新的dev分支，然后切换到dev分支
			//git checkout命令加上-b参数表示创建并切换，相当于一下两条命令
			$ git branch dev
			$ git checkout dev
			
			$ git branch //查看当前分支，git branch 命令会列出所有分支，当前分支前面会标一个*号
			
			$ git checkout master //切换回 rmaster分支
			$ git merge dev //用户合并指定分支到当前的分支
			$ git branch -d dev //删除分支dev
			$ git push origin -d dev //删除远程分支dev
			//当Git无法自动合并分支时，就必须首先解决冲突，解决冲突之后，再提交合并完成。
			$ git log --graph --pretty=oneline --abbrev-commit //查看到分支合并图
		3.4分支管理策略
			通常，合并分支时，Git会用Fast forward模式，但是这种模式下，删除分支后，会丢掉分支信息。
			如果要强制禁用Fast forward模式，Git就会在merge时声称一个新的commit，这样，从分支历史上就可以看出分支信息、
			$ git merge --no-ff -m"merge with no-ff" dev // 合并分支dev，--no-ff 表示禁用Fast forward模式，因为每次合并要创建一个新的commit，所以加上-m参数，把commit描述写进去、
			master分支应该是非常稳定的，也就是仅仅用来发布新版本，平时不在上面干活；
			干活都在dev 分支上，也就是说，dev分支是不稳定的，到某个时候，再把dev分支合并到master上，在master分支发布1.0版本；
			你和你的小伙伴每个人都在分支上干活，每个人都有自己的分支，时不时的往dev分支上合并就可以啦。
			合并分支时，加上--no-ff参数就可以用普通模式合并，合并后的历史有分支，能看出来曾经做过合并，而fast forward合并就看不出来曾经做过合并。
		3.5Bug分支
			在Git中，由于分支如此强大，所以每一个bug都可以通过一个新的临时分支来修复，修复后，合并分支，然后删除临时分支。
			$ git stash //把当前的工作现场储藏起来，等以后恢复线程后继续工作。
			$ git status //现在查看工作区，是干净的
			//假设需要在mastere分支上修复，就从master创建临时分支
			$ git checkout master 
			$ git checkout -b issue-01
			//Bug修复之后，提交更新
			$ git add readme.txt
			$ git commit -m"fix bug 101"
			//修复完成之后，切换会master分支
			$ git checkout master
			//禁用Fast forward模式，合并分支
			$ git merge --no-ff -m"merge fix bug 101" issue-01
			//切换会dev继续工作
			$ git checkout dev
			//查看储藏的工作现场
			$ git stash list
			// 工作现场还在，但是Git把stash内容存储在某个地方了，需要恢复一下，有两种办法
			//一是用 git stash apply 恢复之后，stash内容并不删除，需要使用 git stash drop 删除
			//另一种使用gti stash pop,恢复的同时把stash内容也删除了
			$ git stash pop
			//你可以多次stash，恢复的时候，先用git stash list查看，然后恢复指定的stash，用命令：
			$ git stash apply stash@{0}
		3.6Feature分支
			当添加一个新功能时，你不希望因为一些实验性质的代码，把主分支搞乱了，所以每添加一个新功能，最好新建一个feature分支，在上面完成之后，合并最后删除该feature分支。
			如果要丢弃一个没有被合并过的分支，可以通过git branch -D <name>强行删除。
			$ git branch -D feature-vulcan
		3.7多人协作
			3.7.1远程仓库默认名称是：origin
				$ git remote //查看远程库信息
				$ git remote -v  //查看远程库的详细信息
			3.7.2推送分支
				推送分支就是把该分支上所有本地提交更新到远程库。推送时，要指定本地分支，这样Git就会把该分支推送到远程库对应的远程分支上
				$ git push origin master //推送master分支到远程库
				$ git push origin dev  //推送dev分支到远程库
				但是，并不是一定要把本地分支往远程推送，那么，哪些分支需要推送，哪些不需要呢？
				master分支是主分支，因此要时刻与远程同步；	
				dev分支是开发分支，团队所有成员都需要在上面工作，所以也需要与远程同步；				
				bug分支只用于在本地修复bug，就没必要推到远程了，除非老板要看看你每周到底修复了几个bug；				
				feature分支是否推到远程，取决于你是否和你的小伙伴合作在上面开发。				
				总之，就是在Git中，分支完全可以在本地自己藏着玩，是否推送，视你的心情而定！
			3.7.3抓取分支
				$ git checkout -b dev origin/dev //创建远程origin仓库的dev分支到本地仓库
				$ git branch  --set-upstream dev origin/dev //指定本地dev分支与远程分支的链接
				$ git pull  //抓取分支
				
				多人协作的工作模式通常是这样：

				首先，可以试图用git push origin branch-name推送自己的修改；

				如果推送失败，则因为远程分支比你的本地更新，需要先用git pull试图合并；

				如果合并有冲突，则解决冲突，并在本地提交；

				没有冲突或者解决掉冲突后，再用git push origin branch-name推送就能成功！
		
				如果git  pull 提示"no tracking information",则说明本地分支和远程分支的链接关系没有创建，使用命令 git branch --set-upstream branch-name orgin/branch-name.
		4.标签管理
			4.1创建标签
				$ git tag <name>  //创建一个新标签
				$ git tag v1.0
				$ git tag  //查看所有标签
				$ git tag <name> commitid //创建已提交的commitID 的标签
				$ git show <name>  //查看标签信息
				$ git tag -a v1.0 -m"version 0.1 released" commitid  //创建带有说明的标签，用-a指定标签名，-m指定说明文字
				$ git tag -s <tagname> -m "blablabla..."可以用PGP签名标签；
			4.2操作标签
				$ git tag -d v0.1 //删除标签
				$ git push origin v0.1 //推送壹角标签到远程
				$ git push origin --tags  //一次性推送全部尚未推送到远程的本地标签
				如果标签已经推送到远程，要删除标签的话，先从本地删除：
				$ git tag -d v0.9
				$ git push origin :refs/tags/<tagName> //从删除远程的已提交的标签
		
		
		
		
		
		
		
		
		
		
		
		
		
		
		
		
		
		
		
		
		
		
		
		
		
		
		
		
		
		
		
		
		
		
		
		
		
		
		
		
		
		
		
		
		
		
		
		
		
		
		
		
		
		
		
		
		
	 