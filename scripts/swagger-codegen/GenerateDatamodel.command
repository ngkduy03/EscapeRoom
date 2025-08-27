#!/bin/sh

modelsPath="ret/EscapeRoom/API/Model/"
apiPath="../../src/EscapeRoom/Assets/EscapeRoom/Scripts/API/Model"

testsPathSrc="ret/API/Models/Test/"
testsPathDst="../../src/EscapeRoom/Assets/EscapeRoom/Scripts/Editor/API/Model/Tests"

source ./config.sh

curl --remote-name https://apidocs.saritasa.io/EscapeRoom/develop/EscapeRoom-latest.yaml -k

java -cp "UnityCodegen-swagger-codegen-1.0.0.jar;swagger-codegen-cli.jar" io.swagger.codegen.v3.cli.SwaggerCodegen generate \
-l UnityCodegen \
-i EscapeRoom-latest.yaml \
-c config.json \
-o ret

# It is imposible to store Guid in scenes, and there is no significant benifit from using Guid
find "$modelsPath/." -type f -exec sed -i 's/Guid?/string/g' {} +
find "$modelsPath/." -type f -exec sed -i 's/Guid/string/g' {} +

# Not required for non-windows users.
if ! [[ "$(uname)" == "Darwin" ]]; then
    find "$modelsPath/." -type f -exec unix2dos {} \;
fi

rm -r $apiPath/*

for i in "${API_MODELS[@]}"
do
    # Uncomment if you need to add license info to the file
    # ./AddLicenseInfo.command $i
    cp "$i" "$apiPath"
done

for i in "${API_MODELS_TEST[@]}"
do
    # Uncomment if you need to add license info to the file
    # ./AddLicenseInfo.command $i
    cp "$i" "$testsPathDst"
done

rm -rf ret
