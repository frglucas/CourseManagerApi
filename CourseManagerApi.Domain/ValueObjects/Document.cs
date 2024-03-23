using CourseManagerApi.Domain.Enums;
using CourseManagerApi.Domain.Utils;
using CourseManagerApi.Shared.ValueObjects;
using Flunt.Validations;

namespace CourseManagerApi.Domain.ValueObjects;

public class Document : ValueObject
{
    public Document(string number, EDocumentType documentType)
    {
        Number = DocumentUtils.FilterOnlyNumbers(number.Trim());
        EncryptNumber = String.Empty;
        DocumentType = documentType;

        VerifyNotifications();
    }

    public string Number { get; private set; }
    public string EncryptNumber { get; private set; }
    public EDocumentType DocumentType { get; private set; }

    public void GenerateEncryptNumber(byte[] key, byte[] iv) => EncryptNumber = EncryptUtils.Encrypt(Number, key, iv);

    public override bool Validate()
    {
        VerifyNotifications();
        return IsValid;
    }

    protected override void VerifyNotifications()
    {
        if (VerifyNullValuesToNotifications())
            AddNotifications(
                new Contract<Document>()
                    .Requires()
                    .IsTrue(DocumentType.Validate(Number), "Document.Number", "Número do documento inválido")
            );
    }

    protected override bool VerifyNullValuesToNotifications()
    {
        AddNotifications(
            new Contract<Name>()
                .Requires()
                .IsNotNullOrEmpty(Number, "Document.Number", "Número do documento inválido")
                .IsNotNullOrEmpty(DocumentType.ToString(), "Document.DocumentType", "Tipo do documento inválido")
        );

        return IsValid;
    }
}