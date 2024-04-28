#!/bin/bash

# 定义要打包的操作系统列表
platforms=("linux-x64")

# 定义发布配置
#publish_config="-c Release --self-contained true -p:PublishSingleFile=true -p:PublishTrimmed=true"
publish_config="-c Release"

# 循环遍历操作系统列表
for platform in "${platforms[@]}"
do
    # 构建应用程序包
    dotnet publish $publish_config -r $platform  -o publish/$platform
    
    # 检查构建是否成功
    if [ $? -eq 0 ]; then
        echo "应用程序包构建成功：$platform"
    else
        echo "应用程序包构建失败：$platform"
        exit 1
    fi
done
