const PROXY_CONFIG = [
  {
    context: [
      "/employees"
    ],
    target: 'https://localhost:7032',
    secure: true
  }
]

module.exports = PROXY_CONFIG;
