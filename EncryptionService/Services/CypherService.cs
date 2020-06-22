using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EncryptionService.Services;
using Grpc.Core;
using Microsoft.Extensions.Logging;

namespace EncryptionService
{
    public class CypherService : Cypher.CypherBase
    {
        private readonly ILogger<CypherService> _logger;
        private readonly IEncryption encryptionService;

        private string keyToken = "ORG7kyoyiRaYrg+qBrRrEVhsjJzOfECp7t+8RXq5RJ4=";
        private string ivToken = "sZQaz6kGRbKCjrjoqpy30w==";
        public CypherService(ILogger<CypherService> logger, IEncryption encrypt)
        {
            _logger = logger;
            encryptionService = encrypt;
        }


        public override  Task<EncryptedData> Encrypt(PlainTextData request, ServerCallContext context)
        {
            return Task.FromResult(new EncryptedData
            {
                
                UserName = encryptionService.Encrypt(request.UserName,keyToken,ivToken),
                Password = encryptionService.Encrypt(request.Password,keyToken,ivToken)
            });
        }

        public override Task<PlainTextData> Decrypt(EncryptedData request, ServerCallContext context)
        {
            return Task.FromResult(new PlainTextData
            {
                UserName = encryptionService.Decrypt(request.UserName, keyToken,ivToken),
                Password = encryptionService.Decrypt(request.Password, keyToken,ivToken)
            });
        }

        
    }
}
