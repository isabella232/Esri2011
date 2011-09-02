[remote "origin"]
	fetch = +refs/heads/*:refs/remotes/origin/*
	puttykeyfile = C:/Users/apf/.ssh/apf-esri.putty.ppk
	url = git@github.com:Esri/Esri2011.git
[branch "master"]
	remote = origin
	merge = refs/heads/master

[remote "originesride2011"]
	fetch = +refs/heads/*:refs/remotes/originesride2011/*
	puttykeyfile = C:/Users/apf/.ssh/apf-esri.putty.ppk
	url = https://esri-de@github.com/esri-de/Esri2011.git
[branch "esride2011"]
	remote = originesride2011
	merge = refs/heads/master
