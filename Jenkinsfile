pipeline{
    agent any

    environment {
        DOTNET_VERSION = '8.0'
        CONFIGURATION = 'Release'
        WEB_PROJECT = 'MyAspNetApp/MyAspNetApp.csproj'
        TEST_PROJECT = 'MyAspNetApp.Tests/MyAspNetApp.Tests.csproj'
        PUBLISH_DIR = 'publish_output'
        // DEPLOY_DIR = '${WORKSPACE}/deploy_output'  
        DEPLOY_DIR = '/var/www/myaspnetapp2'
    }
 
    stages
    {
        stage("Debug Info") {
            steps {
                sh "whoami"
                sh "ls -la /var/www"
            }
        }

        stage("Clean")
        {
            steps
            {
                echo "Cleaning up..."
                sh "rm -rf ${PUBLISH_DIR}"
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

        stages {
            stage("Build & Test") {
                parallel {
                    stage("Build") {
                        steps {
                            sh "dotnet build ${WEB_PROJECT} --configuration ${CONFIGURATION} --no-restore"
                        }
                    }
                    stage("Test") {
                        steps {
                            sh "dotnet test ${TEST_PROJECT} --no-build"
                        }
                    }
                }
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

        // Ini perlu akses root
        // stage("Deploy")
        // {
        //     steps
        //     {
        //         sh "mkdir -p ${DEPLOY_DIR}"
        //         sh "cp -r ${PUBLISH_DIR}/* ${DEPLOY_DIR}/"
        //         sh "chown -R www-data:www-data ${DEPLOY_DIR}"

        //         sh "sudo systemctl restart myaspnetapp"
        //         sh "sudo systemctl status myaspnetapp --no-pager"
        //     }
        // }
        stage("Deploy") {
            steps {
                echo "Cleaning up old deployment files..."
                sh "rm -rf ${DEPLOY_DIR}/*"

                echo "Copying published files to deployment directory..."
                sh "cp -r ${PUBLISH_DIR}/* ${DEPLOY_DIR}/"

                // Restart services
                echo "restart services..."
                sh "sudo systemctl restart myaspnetapp"
                sh "sudo systemctl status myaspnetapp --no-pager"
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