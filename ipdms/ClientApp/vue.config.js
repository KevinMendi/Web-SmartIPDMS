module.exports = {
  // The URL where the .NET Core app will be listening.
  // Specific port depends on whether IISExpress/Kestrel and HTTP/HTTPS are used
    runtimeCompiler: true,
    productionSourceMap: false,

    publicPath: process.env.NODE_ENV === 'production'
        ? './'
        : '/',
  devServer: {
    proxy: 'http://localhost:50599'
  },
}