pipeline{
    agent any

    environment {
        DOTNET_VERSION = '8.0'
        CONFIGURATION = 'Release'
        WEB_PROJECT = 'MyAspNetApp/MyAspNetApp.csproj'
        TEST_PROJECT = 'MyAspNetApp.Tests/MyAspNetApp.Tests.csproj'
        PUBLISH_DIR = 'publish_output'
        DEPLOY_DIR = '${WORKSPACE}/deploy_output'  
    }
 
    stages
    {
        stage("Cekout")
        {
            steps
            {
                echo "Checking out source code ...."
            }
        }

        stage("Restore")
        {
            steps
            {
                echo "Restoring packages...."
                sh "dotnet restore ${WEB_PROJECT}"
            }
        }

        stage("Build")
        {
            steps
            {
                echo "Building solution"
                sh "dotnet build ${WEB_PROJECT} --configuration ${CONFIGURATION} --no-restore"
            }
        }

        stage("Test")
        {
            steps
            {
                echo "Running unit test..."
                sh "dotnet test ${TEST_PROJECT} --no-build --verbosity normal"
            }
        }

        stage("Publish")
        {
            steps
            {
                echo "Publish aplication..."
                sh "dotnet publish ${WEB_PROJECT} -c ${CONFIGURATION} -o ${PUBLISH_DIR}"
            }
        }

        stage("Deploy")
        {
            steps
            {
                echo "Deploying to ${DEPLOY_DIR}..."
                sh "mkdir -p ${DEPLOY_DIR}"
                sh "cp -r ${PUBLISH_DIR}/* ${DEPLOY_DIR}"
            }
        }
    }

    post
    {
        success
        {
            echo "Build success"
        }
        failure
        {
            echo "Build failed"
        }
        always
        {
            echo "Build finished"
        }
    }
 
}