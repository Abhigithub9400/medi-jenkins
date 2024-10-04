const { defineConfig } = require('@vue/cli-service');
const path = require('path');

module.exports = defineConfig({
  transpileDependencies: true,
  
  // Set the directory where the built files will be placed
  outputDir: path.resolve(__dirname, '../wwwroot'),

  // Define the base URL for your project
  publicPath: '/',

  // Additional configuration options if needed
  devServer: {
    proxy: 'http://localhost:5000'  // Ensure this matches your .NET backend server URL
  }
});
