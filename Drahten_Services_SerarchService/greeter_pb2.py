# -*- coding: utf-8 -*-
# Generated by the protocol buffer compiler.  DO NOT EDIT!
# source: greeter.proto
# Protobuf Python Version: 4.25.1
"""Generated protocol buffer code."""
from google.protobuf import descriptor as _descriptor
from google.protobuf import descriptor_pool as _descriptor_pool
from google.protobuf import symbol_database as _symbol_database
from google.protobuf.internal import builder as _builder
# @@protoc_insertion_point(imports)

_sym_db = _symbol_database.Default()




DESCRIPTOR = _descriptor_pool.Default().AddSerializedFile(b'\n\rgreeter.proto\x12\x07greeter\"\x16\n\x14\x46indDocumentsRequest\"\x86\x01\n\x08\x44ocument\x12\x11\n\tarticleId\x18\x01 \x01(\t\x12\x11\n\tprevTitle\x18\x02 \x01(\t\x12\r\n\x05title\x18\x03 \x01(\t\x12\x0f\n\x07\x63ontent\x18\x04 \x01(\t\x12\x16\n\x0epublishingDate\x18\x05 \x01(\t\x12\x0e\n\x06\x61uthor\x18\x06 \x01(\t\x12\x0c\n\x04link\x18\x07 \x01(\t\"N\n\x10\x44ocumentResponse\x12\x15\n\rdocumentTopic\x18\x01 \x01(\t\x12#\n\x08\x64ocument\x18\x02 \x01(\x0b\x32\x11.greeter.Document\"2\n\x17SimilarityScoreResponse\x12\x17\n\x0fsimilarityScore\x18\x01 \x01(\x01\x32]\n\x0e\x44ocumentFinder\x12K\n\rFindDoucments\x12\x1d.greeter.FindDocumentsRequest\x1a\x19.greeter.DocumentResponse0\x01\x32i\n\x17\x44ocumentSimilarityCheck\x12N\n\x17\x43heckDocumentSimilarity\x12\x11.greeter.Document\x1a .greeter.SimilarityScoreResponseb\x06proto3')

_globals = globals()
_builder.BuildMessageAndEnumDescriptors(DESCRIPTOR, _globals)
_builder.BuildTopDescriptorsAndMessages(DESCRIPTOR, 'greeter_pb2', _globals)
if _descriptor._USE_C_DESCRIPTORS == False:
  DESCRIPTOR._options = None
  _globals['_FINDDOCUMENTSREQUEST']._serialized_start=26
  _globals['_FINDDOCUMENTSREQUEST']._serialized_end=48
  _globals['_DOCUMENT']._serialized_start=51
  _globals['_DOCUMENT']._serialized_end=185
  _globals['_DOCUMENTRESPONSE']._serialized_start=187
  _globals['_DOCUMENTRESPONSE']._serialized_end=265
  _globals['_SIMILARITYSCORERESPONSE']._serialized_start=267
  _globals['_SIMILARITYSCORERESPONSE']._serialized_end=317
  _globals['_DOCUMENTFINDER']._serialized_start=319
  _globals['_DOCUMENTFINDER']._serialized_end=412
  _globals['_DOCUMENTSIMILARITYCHECK']._serialized_start=414
  _globals['_DOCUMENTSIMILARITYCHECK']._serialized_end=519
# @@protoc_insertion_point(module_scope)
