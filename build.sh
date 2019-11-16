#!/bin/bash

imageName=git.core.web
containerName=git.core.web

cd /var/jenkins_home/workspace/pipeline/
echo "当前路径：$PWD"

echo "尝试删除容器"
if docker ps -a | grep $containerName ;then
        echo "存在容器$containerName"
        docker rm -f $containerName
        echo "删除容器$containerName成功"
fi

echo "尝试删除镜像"
if docker images | grep $imageName ; then
        echo "存在镜像$imageName"
        docker rmi -f $imageName
        echo "删除镜像$imageName成功"
fi

echo "构建镜像"
docker build -t $imageName .

echo "运行容器"
docker run -d -p 12701:80 --name $containerName $imageName
