using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using AuthenticationService;
using AuthenticationService.Models;

namespace AuthenticationService.Test
{
    public class AuthenticationServiceTest
    {
        [Test]
        public void GenerateTokensWithSameSecrectKeyTest()
        {
            // token 1
            var model1 = new JWTContainerModel
            {
                Claims = new Claim[]
                {
                    new Claim(ClaimTypes.Name, "John Doe"),
                    new Claim(ClaimTypes.Email, "johndoe@company.any")
                }
            };
            var authService1 = new JWTService();
            var token1 = authService1.GenerateToken(model1);

            // token 2
            var model2 = new JWTContainerModel
            {
                Claims = new Claim[]
                {
                    new Claim(ClaimTypes.Name, "John Doe 1"),
                    new Claim(ClaimTypes.Email, "johndoe1@company.any")
                }
            };
            var authService2 = new JWTService();
            var token2 = authService2.GenerateToken(model2);

            // token 1 is not equal token 2
            Assert.AreNotEqual(token1, token2);
        }

        [Test]
        public void GenerateTokensWithDifferentSecrectKeyTest()
        {
            // token 1
            var model1 = new JWTContainerModel
            {
                Claims = new Claim[]
                {
                    new Claim(ClaimTypes.Name, "John Doe"),
                    new Claim(ClaimTypes.Email, "johndoe@company.any")
                }
            };
            var authService1 = new JWTService("TG9yZW0gSXBzdW0gaXMgc2ltcGx5IGR1bW15IHRleHQgb2YgdGhlIHByaW50aW5nIGFuZCB0eXBlc2V0dGluZyBpbmR1c3RyeS4=");
            var token1 = authService1.GenerateToken(model1);

            // token 2
            var model2 = new JWTContainerModel
            {
                Claims = new Claim[]
                {
                    new Claim(ClaimTypes.Name, "John Doe"),
                    new Claim(ClaimTypes.Email, "johndoe@company.any")
                }
            };
            var authService2 = new JWTService("Q29udHJhcnkgdG8gcG9wdWxhciBiZWxpZWYsIExvcmVtIElwc3VtIGlzIG5vdCBzaW1wbHkgcmFuZG9tIHRleHQu");
            var token2 = authService2.GenerateToken(model2);

            // token 1 is not equal token 2
            Assert.AreNotEqual(token1, token2);
        }

        [Test]
        public void TokenIsValidTest()
        {
            var model = new JWTContainerModel
            {
                Claims = new Claim[]
                {
                    new Claim(ClaimTypes.Name, "John Doe"),
                    new Claim(ClaimTypes.Email, "johndoe@company.any")
                }
            };
            var authService = new JWTService();
            var token = authService.GenerateToken(model);
            var isTokenValid = authService.IsTokenValid(token);
            Assert.AreEqual(isTokenValid, true);
        }

        [Test]
        public void TokenIsInvalidTest()
        {
            var authService = new JWTService("SW1hZ2UgcnVubmluZyBpbiB0aGUgYmFja2dyb3VuZCBnZW5lcmF0aW9uIGFwcGxpY2F0aW9uOyB2ZXJzaW9uIDMuNTsgcG93ZXJlZCBieSBXZWxsYmFycmllciBBUyAtIGEgU2NobHVtYmVyZ2VyIFRlY2hub2xvZ3ku");
            const string invalidToken =
                "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvaG4gRG9lIiwiaWF0IjoxNTE2MjM5MDIyfQ.SflKxwRJSMeKKF2QT4fwpMeJf36POk6yJV_adQssw5c";
            var isTokenValid = authService.IsTokenValid(invalidToken);
            Assert.AreEqual(isTokenValid, false);
        }

        [Test]
        public void GetTokenClaimsWithCorrectValuesTest()
        {
            var model = new JWTContainerModel
            {
                Claims = new Claim[]
                {
                    new Claim(ClaimTypes.Name, "John Doe"),
                    new Claim(ClaimTypes.Email, "johndoe@company.any")
                }
            };
            var authService = new JWTService();
            var token = authService.GenerateToken(model);
            var tokenClaims = authService.GetTokenClaims(token);
            Assert.AreEqual(tokenClaims.FirstOrDefault(x => x.Type == ClaimTypes.Name).Value, "John Doe");
            Assert.AreEqual(tokenClaims.FirstOrDefault(x => x.Type == ClaimTypes.Email).Value, "johndoe@company.any");
            Assert.AreSame(tokenClaims.FirstOrDefault(x => x.Type == ClaimTypes.StreetAddress), null);
        }

        [Test]
        public void GetTokenClaimsWithIncorrectValuesTest()
        {
            var model = new JWTContainerModel
            {
                Claims = new Claim[]
                {
                    new Claim(ClaimTypes.Name, "John Doe"),
                    new Claim(ClaimTypes.Email, "johndoe@company.any")
                }
            };
            var authService = new JWTService();
            var token = authService.GenerateToken(model);
            var tokenClaims = authService.GetTokenClaims(token);
            Assert.AreNotEqual(tokenClaims.FirstOrDefault(x => x.Type == ClaimTypes.Name).Value, "John Doe 1");
            Assert.AreNotEqual(tokenClaims.FirstOrDefault(x => x.Type == ClaimTypes.Email).Value, "johndoe1@company.any");
        }

        [SetUp]
        public void Setup()
        {

        }

        [OneTimeSetUp]
        public void OneTimeSetup()
        {

        }

        // Tears down each test data
        [TearDown]
        public void DisposeTest()
        {

        }

        // TestFixture teardown
        [OneTimeTearDown]
        public void DisposeAllObjects()
        {

        }
    }
}
